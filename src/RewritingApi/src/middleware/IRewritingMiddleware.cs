using System.Collections.Generic;
using Mono.Cecil;

namespace RewritingContracts
{
    public interface IRewritingMiddleware
    {
        void Process(AssemblyDefinition targetAssembly, AssemblyDefinition gAssembly);
    }
}