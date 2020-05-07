using Mono.Cecil;
using Mono.Cecil.Cil;

namespace RewritingApi
{
    public interface IMethodUsage
    {
        MethodDefinition CallingMethod { get; }
        Instruction CallReference { get; }
    }
}