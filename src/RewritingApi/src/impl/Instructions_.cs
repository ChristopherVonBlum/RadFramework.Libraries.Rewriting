  

using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;

namespace RewritingApi.impl
{
	public static partial class InstructionExtensions
	{


		public static bool IsStoreArgument(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Starg_S, OpCodes.Starg);
		}

		
		public static byte GetStoreArgumentInstructionIndex(this Instruction storeArgumentInstruction)
		{
			OpCode opCode = storeArgumentInstruction.OpCode;

		
			if (opCode == OpCodes.Starg_S)
			{
				return (byte)storeArgumentInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadArgument(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldarg_S, OpCodes.Ldarg, OpCodes.Ldarg_0, OpCodes.Ldarg_1, OpCodes.Ldarg_2, OpCodes.Ldarg_3);
		}

		
		public static byte GetLoadArgumentInstructionIndex(this Instruction loadArgumentInstruction)
		{
			OpCode opCode = loadArgumentInstruction.OpCode;

		
			if (opCode == OpCodes.Ldarg_0)
			{
				return 0;
			}
		
			if (opCode == OpCodes.Ldarg_1)
			{
				return 1;
			}
		
			if (opCode == OpCodes.Ldarg_2)
			{
				return 2;
			}
		
			if (opCode == OpCodes.Ldarg_3)
			{
				return 3;
			}
		
			if (opCode == OpCodes.Ldarg_S)
			{
				return (byte)loadArgumentInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadArgumentAdress(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldarga_S, OpCodes.Ldarga);
		}

		
		public static byte GetLoadArgumentAdressInstructionIndex(this Instruction loadArgumentAdressInstruction)
		{
			OpCode opCode = loadArgumentAdressInstruction.OpCode;

		
			if (opCode == OpCodes.Ldarga_S)
			{
				return (byte)loadArgumentAdressInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsStoreVariable(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Stloc_S, OpCodes.Stloc, OpCodes.Stloc_0, OpCodes.Stloc_1, OpCodes.Stloc_2, OpCodes.Stloc_3);
		}

		
		public static byte GetStoreVariableInstructionIndex(this Instruction storeVariableInstruction)
		{
			OpCode opCode = storeVariableInstruction.OpCode;

		
			if (opCode == OpCodes.Stloc_0)
			{
				return 0;
			}
		
			if (opCode == OpCodes.Stloc_1)
			{
				return 1;
			}
		
			if (opCode == OpCodes.Stloc_2)
			{
				return 2;
			}
		
			if (opCode == OpCodes.Stloc_3)
			{
				return 3;
			}
		
			if (opCode == OpCodes.Stloc_S)
			{
				return (byte)storeVariableInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadVariable(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldloc_S, OpCodes.Ldloc, OpCodes.Ldloc_0, OpCodes.Ldloc_1, OpCodes.Ldloc_2, OpCodes.Ldloc_3);
		}

		
		public static byte GetLoadVariableInstructionIndex(this Instruction loadVariableInstruction)
		{
			OpCode opCode = loadVariableInstruction.OpCode;

		
			if (opCode == OpCodes.Ldloc_0)
			{
				return 0;
			}
		
			if (opCode == OpCodes.Ldloc_1)
			{
				return 1;
			}
		
			if (opCode == OpCodes.Ldloc_2)
			{
				return 2;
			}
		
			if (opCode == OpCodes.Ldloc_3)
			{
				return 3;
			}
		
			if (opCode == OpCodes.Ldloc_S)
			{
				return (byte)loadVariableInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadVariableAdress(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldloca_S, OpCodes.Ldloca);
		}

		
		public static byte GetLoadVariableAdressInstructionIndex(this Instruction loadVariableAdressInstruction)
		{
			OpCode opCode = loadVariableAdressInstruction.OpCode;

		
			if (opCode == OpCodes.Ldloca_S)
			{
				return (byte)loadVariableAdressInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadField(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldfld);
		}

		
		public static Mono.Cecil.FieldReference GetLoadFieldInstructionFieldReference(this Instruction loadFieldInstruction)
		{
			OpCode opCode = loadFieldInstruction.OpCode;

		
			if (opCode == OpCodes.Ldfld)
			{
				return (Mono.Cecil.FieldReference)loadFieldInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadFieldAdress(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldflda);
		}

		
		public static Mono.Cecil.FieldReference GetLoadFieldAdressInstructionFieldReference(this Instruction loadFieldAdressInstruction)
		{
			OpCode opCode = loadFieldAdressInstruction.OpCode;

		
			if (opCode == OpCodes.Ldflda)
			{
				return (Mono.Cecil.FieldReference)loadFieldAdressInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsStoreField(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Stfld);
		}

		
		public static Mono.Cecil.FieldReference GetStoreFieldInstructionFieldReference(this Instruction storeFieldInstruction)
		{
			OpCode opCode = storeFieldInstruction.OpCode;

		
			if (opCode == OpCodes.Stfld)
			{
				return (Mono.Cecil.FieldReference)storeFieldInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadStaticField(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldsfld);
		}

		
		public static Mono.Cecil.FieldReference GetLoadStaticFieldInstructionFieldReference(this Instruction loadStaticFieldInstruction)
		{
			OpCode opCode = loadStaticFieldInstruction.OpCode;

		
			if (opCode == OpCodes.Ldsfld)
			{
				return (Mono.Cecil.FieldReference)loadStaticFieldInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadStaticFieldAdress(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldsflda);
		}

		
		public static Mono.Cecil.FieldReference GetLoadStaticFieldAdressInstructionFieldReference(this Instruction loadStaticFieldAdressInstruction)
		{
			OpCode opCode = loadStaticFieldAdressInstruction.OpCode;

		
			if (opCode == OpCodes.Ldsflda)
			{
				return (Mono.Cecil.FieldReference)loadStaticFieldAdressInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsStoreStaticField(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Stsfld);
		}

		
		public static Mono.Cecil.FieldReference GetStoreStaticFieldInstructionFieldReference(this Instruction storeStaticFieldInstruction)
		{
			OpCode opCode = storeStaticFieldInstruction.OpCode;

		
			if (opCode == OpCodes.Stsfld)
			{
				return (Mono.Cecil.FieldReference)storeStaticFieldInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadString(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldstr);
		}

		
		public static string GetLoadStringInstructionConstant(this Instruction loadStringInstruction)
		{
			OpCode opCode = loadStringInstruction.OpCode;

		
			if (opCode == OpCodes.Ldstr)
			{
				return (string)loadStringInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadInteger(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldc_I4_S, OpCodes.Ldc_I4, OpCodes.Ldc_I4_0, OpCodes.Ldc_I4_1, OpCodes.Ldc_I4_2, OpCodes.Ldc_I4_3, OpCodes.Ldc_I4_4, OpCodes.Ldc_I4_5, OpCodes.Ldc_I4_6, OpCodes.Ldc_I4_7, OpCodes.Ldc_I4_8);
		}

		
		public static int GetLoadIntegerInstructionConstant(this Instruction loadIntegerInstruction)
		{
			OpCode opCode = loadIntegerInstruction.OpCode;

		
			if (opCode == OpCodes.Ldc_I4_0)
			{
				return 0;
			}
		
			if (opCode == OpCodes.Ldc_I4_1)
			{
				return 1;
			}
		
			if (opCode == OpCodes.Ldc_I4_2)
			{
				return 2;
			}
		
			if (opCode == OpCodes.Ldc_I4_3)
			{
				return 3;
			}
		
			if (opCode == OpCodes.Ldc_I4_4)
			{
				return 4;
			}
		
			if (opCode == OpCodes.Ldc_I4_5)
			{
				return 5;
			}
		
			if (opCode == OpCodes.Ldc_I4_6)
			{
				return 6;
			}
		
			if (opCode == OpCodes.Ldc_I4_7)
			{
				return 7;
			}
		
			if (opCode == OpCodes.Ldc_I4_8)
			{
				return 8;
			}
		
			if (opCode == OpCodes.Ldc_I4_S)
			{
				return (int)loadIntegerInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadLong(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldc_I8);
		}

		
		public static long GetLoadLongInstructionConstant(this Instruction loadLongInstruction)
		{
			OpCode opCode = loadLongInstruction.OpCode;

		
			if (opCode == OpCodes.Ldc_I8)
			{
				return (long)loadLongInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadFloat(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldc_R4);
		}

		
		public static float GetLoadFloatInstructionConstant(this Instruction loadFloatInstruction)
		{
			OpCode opCode = loadFloatInstruction.OpCode;

		
			if (opCode == OpCodes.Ldc_R4)
			{
				return (float)loadFloatInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsLoadDouble(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldc_R8);
		}

		
		public static double GetLoadDoubleInstructionConstant(this Instruction loadDoubleInstruction)
		{
			OpCode opCode = loadDoubleInstruction.OpCode;

		
			if (opCode == OpCodes.Ldc_R8)
			{
				return (double)loadDoubleInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsCall(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Call);
		}

		
		public static Mono.Cecil.MethodReference GetCallInstructionMethod(this Instruction callInstruction)
		{
			OpCode opCode = callInstruction.OpCode;

		
			if (opCode == OpCodes.Call)
			{
				return (Mono.Cecil.MethodReference)callInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsCallVirtual(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Callvirt);
		}

		
		public static Mono.Cecil.MethodReference GetCallVirtualInstructionMethod(this Instruction callVirtualInstruction)
		{
			OpCode opCode = callVirtualInstruction.OpCode;

		
			if (opCode == OpCodes.Callvirt)
			{
				return (Mono.Cecil.MethodReference)callVirtualInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}


		public static bool IsNop(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Nop);
		}

	
		public static bool IsReturn(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ret);
		}

	
		public static bool IsThrow(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Throw);
		}

	
		public static bool IsReThrow(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Rethrow);
		}

	
		public static bool IsLoadMetadataToken(this Instruction instruction)
		{
			return IsOpCode(instruction, OpCodes.Ldtoken);
		}

		
		public static Mono.Cecil.IMemberDefinition GetLoadMetadataTokenInstructionMember(this Instruction loadMetadataTokenInstruction)
		{
			OpCode opCode = loadMetadataTokenInstruction.OpCode;

		
			if (opCode == OpCodes.Ldtoken)
			{
				return (Mono.Cecil.IMemberDefinition)loadMetadataTokenInstruction.Operand;
			}
            
			throw new InvalidOperationException();
		}

		private static bool IsOpCode(Instruction instruction, params OpCode[] checkFor)
        {
            OpCode opCode = instruction.OpCode;

            return checkFor.Contains(opCode);
        }
	}

	public static partial class CreateInstruction
	{
	

		public static Instruction StoreArgument(byte argumentIndex)
	
		{

	
			return Instruction.Create(OpCodes.Starg_S, argumentIndex);
		}


		public static Instruction LoadArgument(byte argumentIndex)
	
		{

				switch(argumentIndex)
			{
			
		
				case 0:
					return Instruction.Create(OpCodes.Ldarg_0);
		
				case 1:
					return Instruction.Create(OpCodes.Ldarg_1);
		
				case 2:
					return Instruction.Create(OpCodes.Ldarg_2);
		
				case 3:
					return Instruction.Create(OpCodes.Ldarg_3);
		
			}
	
			return Instruction.Create(OpCodes.Ldarg_S, argumentIndex);
		}


		public static Instruction LoadArgumentAdress(byte argumentIndex)
	
		{

	
			return Instruction.Create(OpCodes.Ldarga_S, argumentIndex);
		}


		public static Instruction StoreVariable(byte variableIndex)
	
		{

				switch(variableIndex)
			{
			
		
				case 0:
					return Instruction.Create(OpCodes.Stloc_0);
		
				case 1:
					return Instruction.Create(OpCodes.Stloc_1);
		
				case 2:
					return Instruction.Create(OpCodes.Stloc_2);
		
				case 3:
					return Instruction.Create(OpCodes.Stloc_3);
		
			}
	
			return Instruction.Create(OpCodes.Stloc_S, variableIndex);
		}


		public static Instruction LoadVariable(byte variableIndex)
	
		{

				switch(variableIndex)
			{
			
		
				case 0:
					return Instruction.Create(OpCodes.Ldloc_0);
		
				case 1:
					return Instruction.Create(OpCodes.Ldloc_1);
		
				case 2:
					return Instruction.Create(OpCodes.Ldloc_2);
		
				case 3:
					return Instruction.Create(OpCodes.Ldloc_3);
		
			}
	
			return Instruction.Create(OpCodes.Ldloc_S, variableIndex);
		}


		public static Instruction LoadVariableAdress(byte variableIndex)
	
		{

	
			return Instruction.Create(OpCodes.Ldloca_S, variableIndex);
		}


		public static Instruction LoadField(Mono.Cecil.FieldReference field)
	
		{

	
			return Instruction.Create(OpCodes.Ldfld, field);
		}


		public static Instruction LoadFieldAdress(Mono.Cecil.FieldReference field)
	
		{

	
			return Instruction.Create(OpCodes.Ldflda, field);
		}


		public static Instruction StoreField(Mono.Cecil.FieldReference field)
	
		{

	
			return Instruction.Create(OpCodes.Stfld, field);
		}


		public static Instruction LoadStaticField(Mono.Cecil.FieldReference field)
	
		{

	
			return Instruction.Create(OpCodes.Ldsfld, field);
		}


		public static Instruction LoadStaticFieldAdress(Mono.Cecil.FieldReference field)
	
		{

	
			return Instruction.Create(OpCodes.Ldsflda, field);
		}


		public static Instruction StoreStaticField(Mono.Cecil.FieldReference field)
	
		{

	
			return Instruction.Create(OpCodes.Stsfld, field);
		}


		public static Instruction LoadString(string str)
	
		{

	
			return Instruction.Create(OpCodes.Ldstr, str);
		}


		public static Instruction LoadInteger(int int32)
	
		{

				switch(int32)
			{
			
		
				case 0:
					return Instruction.Create(OpCodes.Ldc_I4_0);
		
				case 1:
					return Instruction.Create(OpCodes.Ldc_I4_1);
		
				case 2:
					return Instruction.Create(OpCodes.Ldc_I4_2);
		
				case 3:
					return Instruction.Create(OpCodes.Ldc_I4_3);
		
				case 4:
					return Instruction.Create(OpCodes.Ldc_I4_4);
		
				case 5:
					return Instruction.Create(OpCodes.Ldc_I4_5);
		
				case 6:
					return Instruction.Create(OpCodes.Ldc_I4_6);
		
				case 7:
					return Instruction.Create(OpCodes.Ldc_I4_7);
		
				case 8:
					return Instruction.Create(OpCodes.Ldc_I4_8);
		
			}
	
			return Instruction.Create(OpCodes.Ldc_I4_S, int32);
		}


		public static Instruction LoadLong(long int64)
	
		{

	
			return Instruction.Create(OpCodes.Ldc_I8, int64);
		}


		public static Instruction LoadFloat(float float32)
	
		{

	
			return Instruction.Create(OpCodes.Ldc_R4, float32);
		}


		public static Instruction LoadDouble(double float64)
	
		{

	
			return Instruction.Create(OpCodes.Ldc_R8, float64);
		}


		public static Instruction Call(Mono.Cecil.MethodReference method)
	
		{

	
			return Instruction.Create(OpCodes.Call, method);
		}


		public static Instruction CallVirtual(Mono.Cecil.MethodReference method)
	
		{

	
			return Instruction.Create(OpCodes.Callvirt, method);
		}


		public static Instruction Nop()
	
		{

	
			return Instruction.Create(OpCodes.Nop);
		}


		public static Instruction Return()
	
		{

	
			return Instruction.Create(OpCodes.Ret);
		}


		public static Instruction Throw()
	
		{

	
			return Instruction.Create(OpCodes.Throw);
		}


		public static Instruction ReThrow()
	
		{

	
			return Instruction.Create(OpCodes.Rethrow);
		}


	}

	public static partial class Emit
	{
	    private class EmitContext : IDisposable
		{
		
			private Action<Instruction> emit;
			private Action onDispose;

			public EmitContext(Action<Instruction> emit, Action onDispose)
			{
				this.emit = emit;
				this.onDispose = onDispose;
			}

			public void Emit(Instruction instruction)
			{
				emit(instruction);
			}

			public void Dispose()
			{
				onDispose();
			}
		}

		[ThreadStatic]
		private static Stack<EmitContext> emitContextStack;

		private static EmitContext CurrentEmitContext => emitContextStack.Peek();

		public static IDisposable UseContext(Action<Instruction> emitInstruction)
		{
            if (emitContextStack == null)
            {
                emitContextStack = new Stack<EmitContext>();
            }

            var newContext = new EmitContext(emitInstruction, () => emitContextStack.Pop());

			emitContextStack.Push(newContext);

			return newContext;
		}

		public static IDisposable UseContext(ILProcessor processor)
		{
			return UseContext(instruction => processor.Append(instruction));
		}

		public static void Append(Instruction instruction)
		{
			CurrentEmitContext.Emit(instruction);
		}

	
		public static void StoreArgument(byte argumentIndex)
		{
			CurrentEmitContext.Emit(CreateInstruction.StoreArgument(argumentIndex));
		}
	
	
		public static void LoadArgument(byte argumentIndex)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadArgument(argumentIndex));
		}
	
	
		public static void LoadArgumentAdress(byte argumentIndex)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadArgumentAdress(argumentIndex));
		}
	
	
		public static void StoreVariable(byte variableIndex)
		{
			CurrentEmitContext.Emit(CreateInstruction.StoreVariable(variableIndex));
		}
	
	
		public static void LoadVariable(byte variableIndex)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadVariable(variableIndex));
		}
	
	
		public static void LoadVariableAdress(byte variableIndex)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadVariableAdress(variableIndex));
		}
	
	
		public static void LoadField(Mono.Cecil.FieldReference field)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadField(field));
		}
	
	
		public static void LoadFieldAdress(Mono.Cecil.FieldReference field)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadFieldAdress(field));
		}
	
	
		public static void StoreField(Mono.Cecil.FieldReference field)
		{
			CurrentEmitContext.Emit(CreateInstruction.StoreField(field));
		}
	
	
		public static void LoadStaticField(Mono.Cecil.FieldReference field)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadStaticField(field));
		}
	
	
		public static void LoadStaticFieldAdress(Mono.Cecil.FieldReference field)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadStaticFieldAdress(field));
		}
	
	
		public static void StoreStaticField(Mono.Cecil.FieldReference field)
		{
			CurrentEmitContext.Emit(CreateInstruction.StoreStaticField(field));
		}
	
	
		public static void LoadString(string str)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadString(str));
		}
	
	
		public static void LoadInteger(int int32)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadInteger(int32));
		}
	
	
		public static void LoadLong(long int64)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadLong(int64));
		}
	
	
		public static void LoadFloat(float float32)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadFloat(float32));
		}
	
	
		public static void LoadDouble(double float64)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadDouble(float64));
		}
	
	
		public static void Call(Mono.Cecil.MethodReference method)
		{
			CurrentEmitContext.Emit(CreateInstruction.Call(method));
		}
	
	
		public static void CallVirtual(Mono.Cecil.MethodReference method)
		{
			CurrentEmitContext.Emit(CreateInstruction.CallVirtual(method));
		}
	
	
		public static void Nop()
		{
			CurrentEmitContext.Emit(CreateInstruction.Nop());
		}
	
	
		public static void Return()
		{
			CurrentEmitContext.Emit(CreateInstruction.Return());
		}
	
	
		public static void Throw()
		{
			CurrentEmitContext.Emit(CreateInstruction.Throw());
		}
	
	
		public static void ReThrow()
		{
			CurrentEmitContext.Emit(CreateInstruction.ReThrow());
		}
	
	
		public static void LoadMetadataToken(Mono.Cecil.IMemberDefinition member)
		{
			CurrentEmitContext.Emit(CreateInstruction.LoadMetadataToken(member));
		}
	

	}
}

