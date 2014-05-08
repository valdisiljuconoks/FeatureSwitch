namespace FeatureSwitch
{
    using System;

    public interface IDependencyContainer
    {
        void RegisterType(Type requestedType, Type implementation);

        object Resolve(Type type);
    }
}