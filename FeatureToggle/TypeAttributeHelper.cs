using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FeatureToggle
{
    public class TypeAttributeHelper
    {
        public static IEnumerable<Type> GetTypesChildOf<T>()
        {
            var allTypes = new List<Type>();
            foreach (var assembly in GetAssemblies())
            {
                allTypes.AddRange(GetTypesChildOfInAssembly(typeof(T), assembly));
            }

            return allTypes;
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private static IEnumerable<Type> GetTypesChildOfInAssembly(Type type, Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(t => IsTypeChild(t, type) && !t.IsAbstract);
            }
            catch (Exception)
            {
                // there could be situations when type could not be loaded
                // this may happen if we are visiting *all* loaded assemblies in application domain
                return new List<Type>();
            }
        }

        private static bool IsTypeChild(Type target, Type baseClass)
        {
            return target.IsSubclassOf(baseClass);
        }
    }
}
