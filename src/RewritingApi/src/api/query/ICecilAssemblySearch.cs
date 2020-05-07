using System.Collections.Generic;
using Mono.Cecil;

namespace RewritingApi
{
    public interface ICecilAssemblySearch
    {
        IEnumerable<IMethodUsage> FindMethodUsages(AssemblyDefinition targetAssembly, MethodReference method);
    }
}