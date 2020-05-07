using Mono.Cecil;
using Mono.Cecil.Cil;

namespace RewritingApi.impl
{
    public class MethodUsage : IMethodUsage
    {
        public MethodDefinition CallingMethod { get; set; }

        public Instruction CallReference { get; set; }
    }
}