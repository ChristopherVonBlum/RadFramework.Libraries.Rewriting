using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using RewritingApi;
using RewritingContracts;

namespace RewritingJobs
{
    public class AuthorizationProxyGenerator : IRewritingMiddleware
    {
        private readonly IAssemblyQueryProvider _assemblyQueryProvider;

        public AuthorizationProxyGenerator(IAssemblyQueryProvider assemblyQueryProvider)
        {
            _assemblyQueryProvider = assemblyQueryProvider;
        }
        


        public void Process(AssemblyDefinition sourceAssembly, AssemblyDefinition gAssembly)
        {
            foreach (var typeDefinition in QueryTargets(sourceAssembly))
            {
                CreateAuthorizationProxyImplementation(typeDefinition, gAssembly);
            }
        }

        private void CreateAuthorizationProxyImplementation(TypeDefinition typeDefinition, AssemblyDefinition gAssembly)
        {
            TypeDefinition proxyDefinition = new TypeDefinition(typeDefinition.Namespace, AuthorizationServiceProxy.GetGName(typeDefinition), TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit);
            
            gAssembly.MainModule.Types.Add(proxyDefinition);
            
            
        }

        private IEnumerable<TypeDefinition> QueryTargets(AssemblyDefinition targetAssembly)
        {
            return _assemblyQueryProvider.QueryCustomAttributeUsagesOfType<TypeDefinition>(typeof(AuthorizationServiceConsumerAttribute), targetAssembly).Select(usage => usage.DeclaringAttributeProvider);
        }
    }
}