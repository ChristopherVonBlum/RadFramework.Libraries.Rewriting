using System.Reflection;
using ZeroFormatter;

namespace RewritingApi.middleware
{
    public class RuntimeMetadataSerializationStorage : IMetadataSerializationStorage
    {
        private IMetadataSerializationStorage storage;
        
        public RuntimeMetadataSerializationStorage(Assembly assembly)
        {
            storage = ZeroFormatterSerializer.Deserialize<BuildTimeMetadataSerializationStorage>(
                assembly.GetManifestResourceStream($"{assembly.FullName.Substring(0, assembly.FullName.IndexOf(","))}.Resources.{nameof(BuildTimeMetadataSerializationStorage)}"));
        }
        
        public object GetIndex(string indexName)
        {
            return storage.GetIndex(indexName);
        }
    }
}