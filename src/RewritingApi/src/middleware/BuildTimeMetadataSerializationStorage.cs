using System;
using System.Collections.Generic;
using ZeroFormatter;

namespace RewritingApi.middleware
{
    [ZeroFormattable]
    public class BuildTimeMetadataSerializationStorage : IBuildTimeIMetadataSerializationStorage
    {
        [Index(0)]
        public virtual ILazyDictionary<string, IndexWrapper> Indexes { get; set; } = new Dictionary<string, IndexWrapper>().AsLazyDictionary();

        public static IMetadataSerializationStorage Create()
        {
            return new BuildTimeMetadataSerializationStorage();
        }

        public object GetIndex(string indexName)
        {
            IndexWrapper wrapper = Indexes[indexName];
            return ZeroFormatterSerializer.NonGeneric.Deserialize(Type.GetType(wrapper.Type), wrapper.Data);
        }

        public bool HasIndex(string indexName)
        {
            return Indexes.ContainsKey(indexName);
        }

        public void EmbedIndex(string indexName, object index)
        {
            var type = index.GetType();
            IndexWrapper wrapper = new IndexWrapper()
            {
                Type = type.FullName,
                Data = ZeroFormatterSerializer.NonGeneric.Serialize(type, index)
            };
            
            Indexes.Add(indexName, wrapper);
        }
    }
}