namespace RewritingApi.middleware
{
    public interface IBuildTimeIMetadataSerializationStorage : IMetadataSerializationStorage
    {
        bool HasIndex(string indexName);
        void EmbedIndex(string indexName, object index);
    }
}