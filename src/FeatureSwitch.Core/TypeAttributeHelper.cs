using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace FeatureSwitch
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
            var discoveredAssemblies = DependencyContext.Default.RuntimeLibraries;
            var result = new List<Assembly>();

            foreach (var assembly in discoveredAssemblies.Where(a => !a.Name.StartsWith("Microsoft") || !a.Name.StartsWith("System")))
            {
                try
                {
                    result.Add(Assembly.Load(new AssemblyName(assembly.Name)));
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        private static IEnumerable<Type> GetTypesChildOfInAssembly(Type type, Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(t => t.GetTypeInfo().IsSubclassOf(type) && !t.GetTypeInfo().IsAbstract);
            }
            catch (Exception)
            {
                // there could be situations when type could not be loaded
                // this may happen if we are visiting *all* loaded assemblies in application domain
                return new List<Type>();
            }
        }
    }
}
