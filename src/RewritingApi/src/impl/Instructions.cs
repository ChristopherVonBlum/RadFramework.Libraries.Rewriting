using System;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace RewritingApi.impl
{
    public static partial class CreateInstruction
    {
        public static Instruction LoadMetadataToken(IMemberDefinition member)
        {
            if (member is MethodReference)
            {
                return Instruction.Create(OpCodes.Ldtoken, (MethodReference)member);
            }

            if (member is FieldReference)
            {
                return Instruction.Create(OpCodes.Ldtoken, (FieldReference)member);
            }

            if (member is TypeReference)
            {
                return Instruction.Create(OpCodes.Ldtoken, (TypeReference)member);
            }

            throw new InvalidOperationException();
        }
    }
}
