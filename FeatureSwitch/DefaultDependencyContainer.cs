using System;
using System.Collections.Generic;

namespace FeatureSwitch
{
    internal class DefaultDependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();

        public void RegisterType(Type requestedType, Type implementation)
        {
            if (this.typeMap.ContainsKey(requestedType))
            {
                this.typeMap[requestedType] = implementation;
            }
            else
            {
                this.typeMap.Add(requestedType, implementation);
            }
        }

        public object Resolve(Type type)
        {
            var resolveType = type;

            Type overrideType;
            if (this.typeMap.TryGetValue(type, out overrideType))
            {
                resolveType = overrideType;
            }

            return Activator.CreateInstance(resolveType);
        }
    }
}
