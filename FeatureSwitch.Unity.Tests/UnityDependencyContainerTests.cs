using Xunit;

namespace FeatureSwitch.Unity.Tests
{
    using Microsoft.Practices.Unity;

    public class UnityDependencyContainerTests
    {
        [Fact]
        public void Container_WhenRegistered_ResolvesMappedType()
        {
            var container = new UnityDependencyContainer();
            container.RegisterType(typeof(BaseType), typeof(DerivedType));

            var instance = container.Resolve(typeof(BaseType));

            Assert.NotNull(instance);
            Assert.IsType<DerivedType>(instance);
        }

        [Fact]
        public void Container_WhenConfigured_ResolvesMappedType()
        {
            var dependencyContainer = new UnityContainer();
            dependencyContainer.RegisterType<BaseType, DerivedType>();

            var container = new UnityDependencyContainer(dependencyContainer);
            
            var instance = container.Resolve(typeof(BaseType));

            Assert.NotNull(instance);
            Assert.IsType<DerivedType>(instance);
        }

        public class BaseType
        {
        }

        public class DerivedType : BaseType
        {
        }
    }
}
