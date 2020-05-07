using ZeroFormatter;

namespace RewritingApi.middleware
{
    [ZeroFormattable]
    public class IndexWrapper
    {
        [Index(0)]
        public virtual string Type { get; set; }
        
        [Index(1)]
        public virtual byte[] Data { get; set; }
    }
}