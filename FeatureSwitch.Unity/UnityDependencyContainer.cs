using System;
using Microsoft.Practices.Unity;

namespace FeatureSwitch.Unity
{
    public class UnityDependencyContainer : IDependencyContainer
    {
        private readonly IUnityContainer unityContainer;

        public UnityDependencyContainer()
            : this(new UnityContainer())
        {
        }

        public UnityDependencyContainer(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public void RegisterType(Type requestedType, Type implementation)
        {
            this.unityContainer.RegisterType(requestedType, implementation);
        }

        public object Resolve(Type type)
        {
            return this.unityContainer.Resolve(type);
        }
    }
}
