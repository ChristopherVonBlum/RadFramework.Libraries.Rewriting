using Mono.Cecil;

namespace RewritingApi
{
    public interface ICustomAttributeUsage
    {
        CustomAttribute Attribute { get; }
        ICustomAttributeProvider DeclaringAttributeProvider { get; }
    }

    public interface ICustomAttributeUsage<out TAttributeProvider> : ICustomAttributeUsage where TAttributeProvider : ICustomAttributeProvider
    {
        new TAttributeProvider DeclaringAttributeProvider { get; }
    }
}