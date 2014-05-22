using System;

namespace FeatureSwitch
{
    public interface IDependencyContainer
    {
        void RegisterType(Type requestedType, Type implementation);

        object Resolve(Type type);
    }
}
