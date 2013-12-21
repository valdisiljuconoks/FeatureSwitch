using FeatureToggle.Strategies;
using FeatureToggle.Tests.Features;
using FeatureToggle.Tests.Strategies;
using StructureMap;
using Xunit;

namespace FeatureToggle.Tests
{
    public class FeatureSetBuilderTests
    {
        [Fact]
        public void BuilderTest_AddMoreFeature_Success()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.AddFeature<MySampleFeatureWithoutStrategy>();
                              ctx.AddFeature<MyFancyStrategySampleFeature>();

                              ctx.ForStrategy<AppSettingsStrategy>().Use<AlwaysTrueStrategyReader>();
                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyReader>();
                          },
                          expression => expression.AddRegistry<UnitTestDependencyRegistry>());

            Assert.True(FeatureContext.IsEnabled<MySampleFeature>(), "MySampleFeature is not enabled");
            Assert.False(FeatureContext.IsEnabled<MySampleFeatureWithoutStrategy>(), "MySampleFeatureWithoutStrategy is not enabled");
            Assert.True(FeatureContext.IsEnabled<MyFancyStrategySampleFeature>(), "MyFancyStrategySampleFeature is not enabled");
        }

        [Fact]
        public void BuilderTest_AddSameFeatureMultipleTimes_NoFailure()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.AddFeature<MySampleFeature>();

                              ctx.ForStrategy<AppSettingsStrategy>().Use<AlwaysTrueStrategyReader>();
                          },
                          expression => expression.AddRegistry<UnitTestDependencyRegistry>());

            var isEnabled = FeatureContext.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }

        [Fact]
        public void BuilderTest_OneFeatureOneStrategy_Success()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<AppSettingsStrategy>().Use<AlwaysTrueStrategyReader>();
                          });

            var isEnabled = FeatureContext.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled, "MySampleFeature is not enabled");
        }

        [Fact]
        public void BuilderTest_RequestForNonRegisteredFeature_FeatureDisabled()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<AppSettingsStrategy>().Use<AlwaysTrueStrategyReader>();
                          },
                          expression => expression.AddRegistry<UnitTestDependencyRegistry>());

            var isEnabled = FeatureContext.IsEnabled<MySampleFeatureWithoutStrategy>();

            Assert.False(isEnabled);
        }
    }
}
