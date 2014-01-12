using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;
using FeatureSwitch.Tests.Features;
using FeatureSwitch.Tests.Strategies;
using StructureMap;
using Xunit;

namespace FeatureSwitch.Tests
{
    public class FeatureSetBuilderWithStrategiesTests
    {
        [Fact]
        public void BuilderTest_AddSameStrategyMultipleTimes_NoFailures()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyImpl>();
                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyImpl>();
                          });

            var isEnabled = container.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }

        [Fact]
        public void  BuilderTest_CustomStrategyWithConstructorParameters_NoFailure()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeatureWithConstructorParameterStrategy>();
                              ctx.ForStrategy<StrategyWithConstructorParameter>().Use<StrategyWithConstructorParameterReader>();
                          },
                          expression => expression.For<ISampleInjectedInterface>().Use<SampleInjectedInterface>());

            Assert.True(container.IsEnabled<MySampleFeatureWithConstructorParameterStrategy>());
            Assert.NotNull(((StrategyWithConstructorParameterReader)builder.GetStrategyImplementation<StrategyWithConstructorParameter>()).Dependency);
        }

        [Fact]
        public void BuilderTest_CustomStrategy_FeatureIsEnabled()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                          {
                              ctx.AddFeature<MyFancyStrategySampleFeature>();
                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyImpl>();
                          });

            var isEnabled = container.IsEnabled<MyFancyStrategySampleFeature>();

            Assert.True(isEnabled, "MyFancyStrategySampleFeature is not enabled");
        }

        [Fact]
        public void BuilderTest_SwapBuiltInStrategy_ContainerHasSwappedStrategy()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                          });

            var strategy = builder.GetStrategyImplementation<AppSettings>();
            Assert.IsAssignableFrom(typeof(AlwaysTrueStrategyImpl), strategy);
        }

        [Fact]
        public void BuilderTest_SwapBuiltInStrategy_NoFailure()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                          });

            var isEnabled = container.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }
    }
}
