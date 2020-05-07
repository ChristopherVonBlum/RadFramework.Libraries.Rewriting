using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Runtime.Versioning;
using Microsoft.Build.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32.SafeHandles;
using Mono.Cecil;
using Nerdbank.MSBuildExtension;
using Newtonsoft.Json;
using RewritingApi;
using RewritingApi.impl;
using RewritingContracts;

namespace RewritingNetCore
{
    
    public class RadFrameworkApplyRewritingTask : ITask
    {
        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }
        private dynamic isolationContext { get; set; }
        public string LibFolder { get; set; }
        public string CoreRewritingMiddleware { get; set; }
        public string CoreRewritingDependencies { get; set; }
        public string MSBuildProjectDirectory { get; set; }
        public string MSBuildProjectFile { get; set; }
        public string IntermediateOutputPath { get; set; }
        public string IntermediateAssemblyName { get; set; }
        
        [Output]
        public string GAssemblyPath { get; set; }
        
        public virtual string IntermediateAssemblyPath => $"{IntermediateOutputPath}{Path.DirectorySeparatorChar}{IntermediateAssemblyName}";

        public bool Execute()
        {
            // isolate away from calling compiler
            var isolation = new BuildIsolationAssemlbyLoadContext(this);

            string entryPointPath = $"{LibFolder}{Path.DirectorySeparatorChar}{Path.GetFileName(new Uri(this.GetType().Assembly.CodeBase).LocalPath)}";

            Type isolatedTaskType = isolation.LoadEquivalent(this.GetType());

            MethodInfo executeInnerInfo = isolatedTaskType.GetMethod(nameof(ExecuteInner));

            object isolatedTask = Activator.CreateInstance(isolatedTaskType);
            
            foreach (var propertyInfo in isolatedTaskType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (propertyInfo.GetSetMethod(true) == null)
                {
                    continue;
                }
                
                propertyInfo.SetValue(isolatedTask, this.GetType().GetProperty(propertyInfo.Name).GetValue(this));
            }
            
            return (bool)executeInnerInfo.Invoke(isolatedTask, new object[0]);
        }
        
        public bool ExecuteInner()
        {
            isolationContext = AssemblyLoadContext.GetLoadContext(this.GetType().Assembly);
            
            string assemblyExtension = Path.GetExtension(IntermediateAssemblyPath);

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(IntermediateAssemblyName);

            AssemblyDefinition assembly = null;
            
            try
            {
                assembly = AssemblyDefinition.ReadAssembly(IntermediateAssemblyPath, new ReaderParameters(ReadingMode.Immediate) { ReadSymbols = true });
            }
            catch
            {
                return false;
            }
            
            AssemblyDefinition gAssembly = CreateGAssembly(nameWithoutExtension, assemblyExtension, assembly);
            
            MemReloadAssembly(ref gAssembly, GAssemblyPath);
            
            RunMiddleware(ref assembly, ref gAssembly);
            
            return true;
        }

        private void MemReloadAssembly(ref AssemblyDefinition definition, string path, bool symbols = false)
        {
            using (var save = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                definition.Write(save, new WriterParameters{ WriteSymbols =  symbols});
                save.Flush();
                save.Close();
            }
            
            definition.Dispose();
            
            definition = AssemblyDefinition.ReadAssembly(path, new ReaderParameters(ReadingMode.Immediate){ReadSymbols = symbols, InMemory = true});
        }
        
        private void RunMiddleware(ref AssemblyDefinition assembly, ref AssemblyDefinition gAssembly)
        {
            var serviceCollection = BuildMiddlewareServiceCollection(out var coreRewritingMiddlewares, out var config);

            IServiceProvider provider = serviceCollection.BuildServiceProvider();

            RunCoreMiddlewares(ref assembly, ref gAssembly, coreRewritingMiddlewares, provider);

            RunConfigMiddlewares(ref assembly, ref gAssembly, config, provider);
        }

        private void RunConfigMiddlewares(ref AssemblyDefinition assembly, ref AssemblyDefinition gAssembly, JsonConfig config,
            IServiceProvider provider)
        {
            foreach (MiddlewareDefinition userMiddleware in config.Middlewares)
            {
                Type middlewareType = isolationContext.LoadFromAssemblyPath(userMiddleware.Assembly).GetType(userMiddleware.Type);

                ((IRewritingMiddleware) provider.GetService(middlewareType)).Process(assembly, gAssembly);
                MemReloadAssembly(ref assembly, IntermediateAssemblyPath, true);
                MemReloadAssembly(ref gAssembly, GAssemblyPath);
            }
        }

        private void RunCoreMiddlewares(ref AssemblyDefinition assembly, ref AssemblyDefinition gAssembly,
            MiddlewareDefinition[] coreRewritingMiddlewares, IServiceProvider provider)
        {
            foreach (MiddlewareDefinition coreMiddleware in coreRewritingMiddlewares)
            {
                var x = coreMiddleware.RuntimeAssembly.GetType(coreMiddleware.Type.Contains(",") ? coreMiddleware.Type.Substring(0, coreMiddleware.Type.IndexOf(",")) : coreMiddleware.Type);
                
                ((IRewritingMiddleware) provider.GetService(x)).Process(assembly,
                    gAssembly);
                MemReloadAssembly(ref assembly, IntermediateAssemblyPath, true);
                 MemReloadAssembly(ref gAssembly, GAssemblyPath);
            }
        }

        private IServiceCollection BuildMiddlewareServiceCollection(out MiddlewareDefinition[] coreRewritingMiddlewares,
            out JsonConfig config)
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            RewritingDependencyDefinition[] coreRewritingDependencies =
                JsonConvert.DeserializeObject<RewritingDependencyDefinition[]>(CoreRewritingDependencies);

            foreach (RewritingDependencyDefinition rewritingDependencyDefinition in coreRewritingDependencies)
            {
                Type type = isolationContext.LoadEquivalent(Type.GetType(rewritingDependencyDefinition.Type));
                
                serviceCollection.Add(ServiceDescriptor.Transient(type,
                    isolationContext.LoadEquivalent(Type.GetType(rewritingDependencyDefinition.Implementation))));
            }

            coreRewritingMiddlewares = JsonConvert.DeserializeObject<MiddlewareDefinition[]>(CoreRewritingMiddleware);

            foreach (MiddlewareDefinition rewritingMiddlewareDefinition in coreRewritingMiddlewares)
            {
                var type = Type.GetType(rewritingMiddlewareDefinition.Type);
                
                Type coreMiddleware = (isolationContext).LoadEquivalent(type);
                
                rewritingMiddlewareDefinition.RuntimeAssembly =coreMiddleware.Assembly;
                
                serviceCollection.Add(ServiceDescriptor.Transient(coreMiddleware, coreMiddleware));
            }

            config = JsonConvert.DeserializeObject<JsonConfig>(
                File.ReadAllText($"{MSBuildProjectDirectory}{Path.DirectorySeparatorChar}rewriting.json"));

            foreach (RewritingDependencyDefinition userDependency in config.Dependencies)
            {
                var dependencyAssembly = isolationContext.LoadFromAssemblyPath(userDependency.Assembly);
                
                var dependencyType = dependencyAssembly.GetType(userDependency.Type);

                var implementationType = dependencyAssembly.GetType(userDependency.Implementation);

                serviceCollection.Add(ServiceDescriptor.Transient(isolationContext.LoadEquivalent(dependencyType), isolationContext.LoadEquivalent(implementationType)));
            }

            foreach (MiddlewareDefinition userMiddleware in config.Middlewares)
            {
                var dependencyAssembly = isolationContext.LoadFromAssemblyPath(userMiddleware.Assembly);

                var dependencyType = dependencyAssembly.GetType(userMiddleware.Type);

                serviceCollection.Add(ServiceDescriptor.Transient(dependencyType, dependencyType));
            }

            return serviceCollection;
        }

        private AssemblyDefinition CreateGAssembly(string nameWithoutExtension, string assemblyExtension,
            AssemblyDefinition assembly)
        {
            GAssemblyPath = $"{IntermediateOutputPath}{Path.DirectorySeparatorChar}{nameWithoutExtension}.g{assemblyExtension}";

            if (File.Exists(GAssemblyPath))
            {
                File.Delete(GAssemblyPath);
            }
            
            AssemblyNameDefinition gAssemblyName = new AssemblyNameDefinition(assembly.FullName, assembly.Name.Version);

            gAssemblyName.Name = assembly.Name.Name + ".g";
            
            AssemblyDefinition gAssembly =
                AssemblyDefinition.CreateAssembly(gAssemblyName, assembly.MainModule.Name, assembly.MainModule.Kind);
            
            var targetFrameworkAttribute = new CustomAttribute(gAssembly.MainModule.ImportReference(typeof(TargetFrameworkAttribute).GetConstructor(new[]{typeof(string)})));
            
            targetFrameworkAttribute.ConstructorArguments.Add(new CustomAttributeArgument(gAssembly.MainModule.TypeSystem.String, ".NETCoreApp,Version=v2.1"));
            
            gAssembly.CustomAttributes.Add(targetFrameworkAttribute);
            
            gAssembly.MainModule.AssemblyReferences.Add(AssemblyNameReference.Parse(assembly.FullName));
            
            return gAssembly;
        }
    }
}