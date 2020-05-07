using System;
using Mono.Cecil;
using RewritingApi;

namespace RewritingContracts
{
    public class AuthorizationServiceProxy
    {
        public static object For(object target, Type serviceInterface)
        {
            Type gType = Type.GetType(GetGName(serviceInterface));

            return null;
        }
        public static string GetGName(TypeDefinition serviceInterface)
        {
            return
                $"{serviceInterface.FullName}_AuthorizationServiceProxy, {GAssembly.GetGName(serviceInterface.Module.Assembly)}";
        }
        public static string GetGName(Type serviceInterface)
        {
            return
                $"{serviceInterface.FullName}_AuthorizationServiceProxy, {GAssembly.GetGName(serviceInterface.Assembly)}";
        }
    }
}