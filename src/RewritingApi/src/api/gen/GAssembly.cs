using System.Reflection;
using Mono.Cecil;

namespace RewritingApi
{
    public static class GAssembly
    {
        public static string GetGName(Assembly assembly)
        {
            return $"{assembly.GetName().Name}.g";
        }
        
        public static string GetGName(AssemblyDefinition assembly)
        {
            return $"{assembly.Name.Name}.g";
        }
    }
}