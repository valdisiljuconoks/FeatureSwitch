using Xunit;

namespace FeatureSwitch.StructureMap.Tests
{
    public class StructureMapDependencyContainerTests
    {
        [Fact]
        public void Container_WhenRegistered_ResolvesMappedType()
        {
            var container = new StructureMapDependencyContainer();
            container.RegisterType(typeof(BaseType), typeof(DerivedType));

            var instance = container.Resolve(typeof(BaseType));

            Assert.NotNull(instance);
            Assert.IsType<DerivedType>(instance);
        }

        [Fact]
        public void Container_WhenConfigured_ResolvesMappedType()
        {
            var container = new StructureMapDependencyContainer();
            container.Configure(c => c.For(typeof(BaseType)).Use(typeof(DerivedType)));

            var instance = container.Resolve(typeof(BaseType));

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
