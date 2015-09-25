using System.Linq;
using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;
using FeatureSwitch.Tests.Features;
using FeatureSwitch.Tests.Strategies;
using Xunit;

namespace FeatureSwitch.Tests
{
    using System;

    public class FeatureSetBuilderWithStrategiesTests
    {
        [Fact]
        public void BuilderTest_AddSameStrategyMultipleTimes_NoFailures()
        {
            var builder = new FeatureSetBuilder();
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
        public void BuilderTest_CustomStrategyWithConstructorParameters_ThrowsExceptionWithSimpleContainer()
        {
            var builder = new FeatureSetBuilder();
            
            Assert.Throws<MissingMethodException>(() => builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeatureWithConstructorParameterStrategy>();
                              ctx.ForStrategy<StrategyWithConstructorParameter>().Use<StrategyWithConstructorParameterReader>();
                          }));
        }

        [Fact]
        public void BuilderTest_CustomStrategy_FeatureIsEnabled()
        {
            var builder = new FeatureSetBuilder();
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
            var builder = new FeatureSetBuilder();
            builder.Build(ctx =>
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
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
                          {
                              ctx.AddFeature<MySampleFeature>();
                              ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                          });

            var isEnabled = container.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }

        [Fact]
        public void BuilderTest_AddFeature_ReturnsEnabledStrategies()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
            {
                ctx.AddFeature<MySampleFeature>();
            });

            var enabledStrategies = container.GetEnabledStateStrategiesForFeature<MySampleFeature>().ToList();

            Assert.True(enabledStrategies.Any());
            Assert.Equal(typeof(AppSettingsStrategyImpl), enabledStrategies.First().GetType());
        }

        [Fact]
        public void BuilderTest_AddFeatureWithoutStrategy_ReturnsNothing()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
            {
                ctx.AddFeature<MySampleFeatureWithoutStrategy>();
            });

            var enabledStrategies = container.GetEnabledStateStrategiesForFeature<MySampleFeatureWithoutStrategy>();

            Assert.False(enabledStrategies.Any());
        }
    }
}
