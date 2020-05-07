namespace TestTarget
{
    public class ProxyProto<TInstance>
    {
        private readonly TInstance instance;

        public ProxyProto(TInstance instance)
        {
            this.instance = instance;
        }
        
        
    }
}