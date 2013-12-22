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
            builder.Build();

            Assert.NotNull(FeatureContext.GetFeature<MySampleDiscoveredFeature>());
        }

        [Fact]
        public void BuilderTest_2ndLevelInheritance_DiscoveredFeatureFound()
        {
            var builder = new FeatureSetBuilder();
            builder.Build();

            Assert.NotNull(FeatureContext.GetFeature<MySample2ndLevelFeature>());
        }
    }

    public class MySample2ndLevelFeature : MySample1stLevelFeature
    {
    }

    public class MySample1stLevelFeature : BaseFeature
    {
    }
}
