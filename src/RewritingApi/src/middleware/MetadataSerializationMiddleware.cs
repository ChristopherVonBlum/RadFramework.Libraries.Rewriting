using System.Linq;
using Mono.Cecil;
using RewritingContracts;
using ZeroFormatter;

namespace RewritingApi.middleware
{
    public class MetadataSerializationMiddleware : IRewritingMiddleware
    {
        public void Process(AssemblyDefinition targetAssembly, AssemblyDefinition gAssembly)
        {
            BuildTimeMetadataSerializationStorage metadataSerializationStorage = new BuildTimeMetadataSerializationStorage();
            
            metadataSerializationStorage.EmbedIndex("test", 123);

            targetAssembly
                .MainModule
                .Resources
                .Where(r => r.Name == nameof(BuildTimeMetadataSerializationStorage))
                .ToList()
                .ForEach(resource => targetAssembly.MainModule.Resources.Remove(resource));
            
            targetAssembly.MainModule.Resources.Add(new EmbeddedResource($"{targetAssembly.FullName.Substring(0, targetAssembly.FullName.IndexOf(","))}.Resources.{nameof(BuildTimeMetadataSerializationStorage)}", ManifestResourceAttributes.Public, ZeroFormatterSerializer.Serialize(metadataSerializationStorage)));
        }
    }
}