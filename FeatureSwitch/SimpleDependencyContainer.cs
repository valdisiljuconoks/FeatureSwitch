using System;
using System.Collections.Generic;

namespace FeatureSwitch
{
    internal class SimpleDependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();

        public void RegisterType(Type requestedType, Type implementation)
        {
            if (typeMap.ContainsKey(requestedType))
            {
                typeMap[requestedType] = implementation;
            }
            else
            {
                typeMap.Add(requestedType, implementation);
            }
        }

        public object Resolve(Type type)
        {
            var resolveType = type;

            Type overrideType;
            if (typeMap.TryGetValue(type, out overrideType))
            {
                resolveType = overrideType;
            }

            return Activator.CreateInstance(resolveType);
        }
    }
}