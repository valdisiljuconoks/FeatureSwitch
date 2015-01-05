using System;
using Ninject;

namespace FeatureSwitch.Ninject
{
    public class NinjectDependencyContainer : IDependencyContainer
    {
        private readonly IKernel _kernel;

        public NinjectDependencyContainer(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void RegisterType(Type requestedType, Type implementation)
        {
            _kernel.Bind(requestedType).To(implementation);
        }

        public object Resolve(Type type)
        {
            return _kernel.Get(type);
        }
    }
}