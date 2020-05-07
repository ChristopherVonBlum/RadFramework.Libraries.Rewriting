using Mono.Cecil;

namespace RewritingApi.impl
{
    public class CustomAttributeUsage<T> : ICustomAttributeUsage<T> where T : ICustomAttributeProvider
    {
        public CustomAttribute Attribute { get; set; }
        public T DeclaringAttributeProvider { get; set; }

        ICustomAttributeProvider ICustomAttributeUsage.DeclaringAttributeProvider => DeclaringAttributeProvider;
    }
}