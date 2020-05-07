using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace RewritingApi.impl
{
    public class AssemblyQueryProvider : IAssemblyQueryProvider
    {
        public IEnumerable<ModuleDefinition> QueryModules(AssemblyDefinition targetAssemly)
        {
            return targetAssemly.Modules;
        }

        public IEnumerable<TypeDefinition> QueryTypes(AssemblyDefinition targetAssemly)
        {
            var assemblyTypes = QueryModules(targetAssemly).SelectMany(m => m.Types).ToList();

            return assemblyTypes.Concat(assemblyTypes.SelectMany(QueryNestedTypes));
        }

        private IEnumerable<TypeDefinition> QueryNestedTypes(TypeDefinition parentType)
        {
            return parentType.NestedTypes.SelectMany(QueryNestedTypes);
        }

        public IEnumerable<TypeDefinition> QueryLayoutedTypes(AssemblyDefinition targetAssemly)
        {
            return QueryTypes(targetAssemly).Where(t => t.IsClass || t.IsInterface || IsStruct(t));
        }

        public IEnumerable<TypeDefinition> QueryClasses(AssemblyDefinition targetAssemly)
        {
            return QueryTypes(targetAssemly).Where(t => t.IsClass && !IsDelegateType(t));
        }

        public IEnumerable<TypeDefinition> QueryStructs(AssemblyDefinition targetAssemly)
        {
            return QueryTypes(targetAssemly).Where(IsStruct);
        }

        private bool IsStruct(TypeDefinition typeDefinition)
        {
            return typeDefinition.IsValueType && !typeDefinition.IsEnum;
        }

        public IEnumerable<TypeDefinition> QueryEnums(AssemblyDefinition targetAssemly)
        {
            return QueryTypes(targetAssemly).Where(t => t.IsEnum);
        }
        public IEnumerable<IMemberDefinition> QueryMembers(AssemblyDefinition targetAssemly)
        {
            return QueryTypes(targetAssemly)
                .SelectMany(t => 
                    t.Events
                        .Concat<IMemberDefinition>(t.Methods)
                        .Concat(t.Properties)
                        .Concat(t.IsInterface 
                            ? new List<IMemberDefinition>()
                            : t.Fields
                                .Concat<IMemberDefinition>(t.NestedTypes)));
        }
        public IEnumerable<MethodDefinition> QueryFunctionMembers(AssemblyDefinition targetAssemly)
        {
            return QueryLayoutedTypes(targetAssemly).SelectMany(t => t.Methods);
        }

        public IEnumerable<MethodDefinition> QueryConstructors(AssemblyDefinition targetAssemly)
        {
            return QueryClasses(targetAssemly).SelectMany(t => t.Methods).Where(f => f.IsConstructor);
        }

        public IEnumerable<MethodDefinition> QueryMethods(AssemblyDefinition targetAssemly)
        {
            return QueryFunctionMembers(targetAssemly).Where(f => !f.IsConstructor);
        }

        private IEnumerable<ICustomAttributeUsage<TAttributeProvider>> GetAttributeUsages<TAttributeProvider>(IEnumerable<TAttributeProvider> providers) where TAttributeProvider : ICustomAttributeProvider
        {
            return providers.SelectMany(t => t.CustomAttributes, (dt, a) => new CustomAttributeUsage<TAttributeProvider> {Attribute = a, DeclaringAttributeProvider = dt});
        }

        public IEnumerable<ICustomAttributeUsage> QueryCustomAttributeUsages(AssemblyDefinition targetAssemly, AttributeTargets filter)
        {
            IEnumerable<ICustomAttributeUsage> attributeQueryResult = new List<ICustomAttributeUsage>();

            var hasAllFlags = filter.HasFlag(AttributeTargets.All);

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Assembly))
            {
                attributeQueryResult = attributeQueryResult.Concat(targetAssemly.CustomAttributes.Select(a => new CustomAttributeUsage<AssemblyDefinition> { Attribute = a, DeclaringAttributeProvider = targetAssemly }));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Module))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryModules(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Class))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryClasses(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Struct))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryStructs(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Enum))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryEnums(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Constructor))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryConstructors(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Method))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryMethods(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Property))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryProperties(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Field))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryFields(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Event))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryEvents(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Interface))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryInterfaces(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Parameter))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryParameters(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.Delegate))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryDelegateTypes(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.ReturnValue))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryReturnTypes(targetAssemly)));
            }

            if (hasAllFlags || filter.HasFlag(AttributeTargets.GenericParameter))
            {
                attributeQueryResult = attributeQueryResult.Concat(GetAttributeUsages(QueryGenericParameters(targetAssemly)));
            }

            return attributeQueryResult;
        }

        public IEnumerable<ICustomAttributeUsage> QueryCustomAttributeUsagesOfType(Type tAttribute, AssemblyDefinition targetAssemly)
        {
            AttributeUsageAttribute usageRules = tAttribute.GetCustomAttributes(true).OfType<AttributeUsageAttribute>().Single();

            return QueryCustomAttributeUsagesOfType(tAttribute, targetAssemly, usageRules.ValidOn);
        }

        public IEnumerable<ICustomAttributeUsage> QueryCustomAttributeUsagesOfType(Type tAttribute, AssemblyDefinition targetAssemly, AttributeTargets filter)
        {
            int attributeTypeToken = tAttribute.MetadataToken;

            return QueryCustomAttributeUsages(targetAssemly, filter).Where(usage => usage.Attribute.AttributeType.MetadataToken.ToInt32().Equals(attributeTypeToken));
        }

        public IEnumerable<ICustomAttributeUsage<TAttributeProvider>> QueryCustomAttributeUsages<TAttributeProvider>(AssemblyDefinition targetAssemly, AttributeTargets filter) where TAttributeProvider : ICustomAttributeProvider
        {
            return QueryCustomAttributeUsages(targetAssemly, filter).OfType<ICustomAttributeUsage<TAttributeProvider>>();
        }

        public IEnumerable<ICustomAttributeUsage<TAttributeProvider>> QueryCustomAttributeUsagesOfType<TAttributeProvider>(Type tAttribute, AssemblyDefinition targetAssemly) where TAttributeProvider : ICustomAttributeProvider
        {
            return QueryCustomAttributeUsagesOfType(tAttribute, targetAssemly).OfType<ICustomAttributeUsage<TAttributeProvider>>();
        }

        public IEnumerable<ICustomAttributeUsage<TAttributeProvider>> QueryCustomAttributeUsagesOfType<TAttributeProvider>(Type tAttribute, AssemblyDefinition targetAssemly, AttributeTargets filter) where TAttributeProvider : ICustomAttributeProvider
        {
            return QueryCustomAttributeUsagesOfType(tAttribute, targetAssemly, filter).OfType<ICustomAttributeUsage<TAttributeProvider>>();
        }

        public IEnumerable<GenericParameter> QueryGenericParameters(AssemblyDefinition targetAssemly)
        {
            return QueryTypes(targetAssemly).SelectMany(t => t.GenericParameters);
        }

        public IEnumerable<MethodReturnType> QueryReturnTypes(AssemblyDefinition targetAssembly)
        {
            return QueryMethods(targetAssembly).Select(m => m.MethodReturnType);
        }

        public IEnumerable<TypeDefinition> QueryDelegateTypes(AssemblyDefinition targetAssemly)
        {
            return QueryLayoutedTypes(targetAssemly).Where(IsDelegateType);
        }

        public IEnumerable<ParameterDefinition> QueryParameters(AssemblyDefinition targetAssemly)
        {
            return QueryFunctionMembers(targetAssemly).Where(f => f.HasParameters).SelectMany(f => f.Parameters);
        }

        public IEnumerable<TypeDefinition> QueryInterfaces(AssemblyDefinition targetAssemly)
        {
            return QueryLayoutedTypes(targetAssemly).Where(t => t.IsInterface);
        }

        public IEnumerable<EventDefinition> QueryEvents(AssemblyDefinition targetAssemly)
        {
            return QueryLayoutedTypes(targetAssemly).Where(t => t.HasEvents).SelectMany(t => t.Events);
        }

        public IEnumerable<FieldDefinition> QueryFields(AssemblyDefinition targetAssemly)
        {
            return QueryLayoutedTypes(targetAssemly).Where(t => t.HasFields).SelectMany(t => t.Fields);
        }

        public IEnumerable<PropertyDefinition> QueryProperties(AssemblyDefinition targetAssemly)
        {
            return QueryLayoutedTypes(targetAssemly).Where(t => t.HasProperties).SelectMany(t => t.Properties);
        }

        public IEnumerable<MethodBody> QueryMethodBodies(AssemblyDefinition targetAssemly)
        {
            return QueryMethods(targetAssemly).Where(m => m.HasBody).Select(m => m.Body);
        }

        private bool IsDelegateType(TypeDefinition typeDefinition)
        {
            return typeDefinition.IsClass && typeDefinition.BaseType != null && typeDefinition.BaseType.FullName == "System.MulticastDelegate";
        }
    }
}