using System;
using System.Collections.Generic;

namespace FeatureSwitch
{
    internal class DefaultDependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<Type, Type> _typeMap = new Dictionary<Type, Type>();

        public void RegisterType(Type requestedType, Type implementation)
        {
            if (_typeMap.ContainsKey(requestedType))
            {
                _typeMap[requestedType] = implementation;
            }
            else
            {
                _typeMap.Add(requestedType, implementation);
            }
        }

        public object Resolve(Type type)
        {
            var resolveType = type;

            Type overrideType;
            if (_typeMap.TryGetValue(type, out overrideType))
            {
                resolveType = overrideType;
            }

            return Activator.CreateInstance(resolveType);
        }
    }
}
