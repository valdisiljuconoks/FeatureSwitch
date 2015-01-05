using Xunit;

namespace FeatureSwitch.Ninject.Tests
{
    public class NinjectDependencyContainerTests
    {
        [Fact]
        public void Container_WhenRegistered_ResolvesMappedType()
        {
            var container = new NinjectDependencyContainer();
            container.RegisterType(typeof (BaseType), typeof (DerivedType));

            var instance = container.Resolve(typeof (BaseType));

            Assert.NotNull(instance);
            Assert.IsType<DerivedType>(instance);
        }
    }

    public class BaseType
    {
    }

    public class DerivedType : BaseType
    {
    }
}