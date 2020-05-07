namespace RewritingApi.middleware
{
    public interface IMetadataSerializationStorage
    {
        object GetIndex(string indexName);
    }
}