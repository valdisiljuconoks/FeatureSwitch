using FeatureToggle.Strategies;
using FeatureToggle.Tests.Features;
using FeatureToggle.Tests.Strategies;
using StructureMap;
using Xunit;
using AlwaysTrueStrategyReader = FeatureToggle.Tests.Strategies.AlwaysTrueStrategyReader;

namespace FeatureToggle.Tests
{
    public class FeatureSetBuilderWithStrategiesTests
    {
        [Fact]
        public void BuilderTest_AddSameStrategyMultipleTimes_NoFailures()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyReader>();
                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyReader>();
                          },
                          expression => expression.AddRegistry<UnitTestDependencyRegistry>());

            var isEnabled = FeatureContext.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }

        [Fact]
        public void  BuilderTest_CustomStrategyWithConstructorParameters_NoFailure()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeatureWithConstructorParameterStrategy>();
                              ctx.ForStrategy<StrategyWithConstructorParameter>().Use<StrategyWithConstructorParameterReader>();
                          },
                          expression => expression.For<ISampleInjectedInterface>().Use<SampleInjectedInterface>());

            var feature = FeatureContext.GetFeature<MySampleFeatureWithConstructorParameterStrategy>();

            Assert.True(feature.IsEnabled);
            Assert.NotNull(((StrategyWithConstructorParameterReader)builder.GetStrategyReader<StrategyWithConstructorParameter>()).Dependency);
        }

        [Fact]
        public void BuilderTest_CustomStrategy_FeatureIsEnabled()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MyFancyStrategySampleFeature>();
                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyReader>();
                          },
                          expression => expression.AddRegistry<UnitTestDependencyRegistry>());

            var isEnabled = FeatureContext.IsEnabled<MyFancyStrategySampleFeature>();

            Assert.True(isEnabled, "MyFancyStrategySampleFeature is not enabled");
        }

        [Fact]
        public void BuilderTest_SwapBuiltInStrategy_ContainerHasSwappedStrategy()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<AppSettingsStrategy>().Use<AlwaysTrueStrategyReader>();
                          },
                          expression => expression.AddRegistry<UnitTestDependencyRegistry>());

            var strategy = builder.GetStrategyReader<AppSettingsStrategy>();
            Assert.True(strategy.GetType().IsAssignableFrom(typeof(AlwaysTrueStrategyReader)));
        }

        [Fact]
        public void BuilderTest_SwapBuiltInStrategy_NoFailure()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<AppSettingsStrategy>().Use<AlwaysTrueStrategyReader>();
                          },
                          expression => expression.AddRegistry<UnitTestDependencyRegistry>());

            var isEnabled = FeatureContext.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }
    }
}
