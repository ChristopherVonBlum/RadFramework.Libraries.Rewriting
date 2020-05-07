using System;
using System.Reflection;
using Mono.Cecil;
using MethodBody = Mono.Cecil.Cil.MethodBody;

namespace RewritingApi.ext
{
    public interface IEmitExtensionFacade
    {
        void AddParameter(MethodDefinition methodInfo, string parameterName, TypeDefinition parameterType);
        void AddAutoProperty(TypeDefinition typeDefinition, string propertyName);
        
        MethodBody CreateInterceptedSetter(
            MethodDefinition method,
            InterceptMethodEntryDelegate[] interceptMethodEntry = null, 
            InterceptMethodExceptionDelegate[] interceptMethodException = null, 
            InterceptMethodExitDelegate[] interceptMethodExit = null);
        
        MethodBody CreateInterceptedGetter(
            MethodDefinition method,
            InterceptMethodEntryDelegate[] interceptMethodEntry = null, 
            InterceptMethodExceptionDelegate[] interceptMethodException = null, 
            InterceptMethodExitDelegate[] interceptMethodExit = null);
    }

    public delegate void InterceptMethodEntryDelegate(MethodBase methodInfo, object[] arguments, ref object returnValue);
    public delegate void InterceptMethodExceptionDelegate(MethodBase methodInfo, object[] arguments, Exception exception, ref object returnValue);
    public delegate void InterceptMethodExitDelegate(MethodBase methodInfo, object[] arguments, ref object returnValue);
}