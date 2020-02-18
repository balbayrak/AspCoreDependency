using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspCoreDependency.Core.Concrete
{
    public static class TypeMapHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblies"></param>
        /// <param name="namespaceStr"></param>
        /// <returns></returns>
        public static IEnumerable<TypeMap> GetTypeMaps<TInterface>(Assembly[] assemblies, string namespaceStr = null)
        {
            List<Assembly> assembliesList = assemblies.ToList();
            IEnumerable<Type> types = assembliesList.SelectMany(asm => asm.DefinedTypes).Select(x => x.AsType());

            if (string.IsNullOrEmpty(namespaceStr?.Trim()))
            {
                types = types.Where(t => t.IsNonAbstractClass(true) && typeof(TInterface).IsAssignableFrom(t));
            }
            else
            {
                types = types.Where(t => t.IsNonAbstractClass(true) && t.Namespace.StartsWith(namespaceStr) && typeof(TInterface).IsAssignableFrom(t));
            }

            Func<TypeInfo, IEnumerable<Type>> selector = t => t.ImplementedInterfaces
                .Where(x => x.HasMatchingGenericParameterCount(t))
                .Select(x => x.GetRegistrationType(t));

            Func<Type, IEnumerable<Type>> selector1 = t => selector(t.GetTypeInfo());
          
            IEnumerable<TypeMap> maps = types.Select(t => new TypeMap(t, selector1(t)));

            return maps;
        }
    }
}
