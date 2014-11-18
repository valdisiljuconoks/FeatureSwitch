using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;
using FeatureSwitch.Tests.Features;
using FeatureSwitch.Tests.Strategies;
using Xunit;

namespace FeatureSwitch.Tests.StrategyTests
{
    public class EnabledStrategyTests
    {
        [Fact]
        public void SingleFeature_SingleStrategy_TestForEnabled()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
            {
                ctx.AddFeature<MySampleFeature>();

                ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
            });

            Assert.True(container.IsStrategyEnabled(typeof(AlwaysTrueStrategyImpl)));
        }

        [Fact]
        public void MultipleFeatures_SameStrategy_TestForEnabled()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
            {
                ctx.AddFeature<MySampleFeature>();
                ctx.AddFeature<MyFancyStrategySampleFeature>();

                ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyImpl>();
            });

            Assert.True(container.IsStrategyEnabled(typeof(AlwaysTrueStrategyImpl)));
        }
    }
}
