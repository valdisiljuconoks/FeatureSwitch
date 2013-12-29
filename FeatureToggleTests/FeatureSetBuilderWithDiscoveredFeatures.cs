using FeatureToggle.Tests.Features;
using Xunit;

namespace FeatureToggle.Tests
{
    public class FeatureSetBuilderWithDiscoveredFeatures
    {
        [Fact]
        public void BuilderTest_BuildContextWithDiscovery_DiscoveredFeatureFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build();

            Assert.NotNull(container.GetFeature<MySampleDiscoveredFeature>());
        }

        [Fact]
        public void BuilderTest_2ndLevelInheritance_DiscoveredFeatureFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context => context.AddFeature<MySample2ndLevelFeature>());

            Assert.NotNull(container.GetFeature<MySample2ndLevelFeature>());
        }
    }

    public class MySample2ndLevelFeature : MySample1stLevelFeature
    {
    }

    public class MySample1stLevelFeature : BaseFeature
    {
    }
}
