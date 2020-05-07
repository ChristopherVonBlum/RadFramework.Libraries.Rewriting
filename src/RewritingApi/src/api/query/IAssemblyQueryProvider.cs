using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace RewritingApi
{
    public interface IAssemblyQueryProvider
    {
        IEnumerable<TypeDefinition> QueryClasses(AssemblyDefinition targetAssemly);
        IEnumerable<MethodDefinition> QueryConstructors(AssemblyDefinition targetAssemly);
        IEnumerable<TypeDefinition> QueryDelegateTypes(AssemblyDefinition targetAssemly);
        IEnumerable<TypeDefinition> QueryEnums(AssemblyDefinition targetAssemly);
        IEnumerable<EventDefinition> QueryEvents(AssemblyDefinition targetAssemly);
        IEnumerable<FieldDefinition> QueryFields(AssemblyDefinition targetAssemly);
        IEnumerable<MethodDefinition> QueryFunctionMembers(AssemblyDefinition targetAssemly);
        IEnumerable<GenericParameter> QueryGenericParameters(AssemblyDefinition targetAssemly);
        IEnumerable<TypeDefinition> QueryInterfaces(AssemblyDefinition targetAssemly);
        IEnumerable<TypeDefinition> QueryLayoutedTypes(AssemblyDefinition targetAssemly);
        IEnumerable<IMemberDefinition> QueryMembers(AssemblyDefinition targetAssemly);
        IEnumerable<MethodBody> QueryMethodBodies(AssemblyDefinition targetAssemly);
        IEnumerable<MethodDefinition> QueryMethods(AssemblyDefinition targetAssemly);
        IEnumerable<ModuleDefinition> QueryModules(AssemblyDefinition targetAssemly);
        IEnumerable<ParameterDefinition> QueryParameters(AssemblyDefinition targetAssemly);
        IEnumerable<PropertyDefinition> QueryProperties(AssemblyDefinition targetAssemly);
        IEnumerable<MethodReturnType> QueryReturnTypes(AssemblyDefinition targetAssembly);
        IEnumerable<TypeDefinition> QueryStructs(AssemblyDefinition targetAssemly);
        IEnumerable<TypeDefinition> QueryTypes(AssemblyDefinition targetAssemly);
        IEnumerable<ICustomAttributeUsage<TAttributeProvider>> QueryCustomAttributeUsages<TAttributeProvider>(AssemblyDefinition targetAssemly, AttributeTargets filter) where TAttributeProvider : ICustomAttributeProvider;
        IEnumerable<ICustomAttributeUsage<TAttributeProvider>> QueryCustomAttributeUsagesOfType<TAttributeProvider>(Type tAttribute, AssemblyDefinition targetAssemly) where TAttributeProvider : ICustomAttributeProvider;
        IEnumerable<ICustomAttributeUsage<TAttributeProvider>> QueryCustomAttributeUsagesOfType<TAttributeProvider>(Type tAttribute, AssemblyDefinition targetAssemly, AttributeTargets filter) where TAttributeProvider : ICustomAttributeProvider;
        IEnumerable<ICustomAttributeUsage> QueryCustomAttributeUsages(AssemblyDefinition targetAssemly, AttributeTargets filter);
        IEnumerable<ICustomAttributeUsage> QueryCustomAttributeUsagesOfType(Type tAttribute, AssemblyDefinition targetAssemly);
        IEnumerable<ICustomAttributeUsage> QueryCustomAttributeUsagesOfType(Type tAttribute, AssemblyDefinition targetAssemly, AttributeTargets filter);
    }
}