using System.Linq;
using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;
using Xunit;

namespace FeatureSwitch.Tests
{
    public class FeatureSetBuilderWithMultipleStrategiesTests
    {
        [Fact]
        public void BuilderTest_MultipleStrategies_FeatureEnabled()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MyFeatureDisabledWithMultipleStrategies>();
                                              ctx.ForStrategy<AppSettings>().Use<AlwaysFalseStrategyImpl>();
                                          });

            Assert.False(container.IsEnabled<MyFeatureDisabledWithMultipleStrategies>());
        }

        [Fact]
        public void BuilderTest_MultipleStrategies_SwapedDefaultStrategy()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MyFeatureDisabledWithMultipleStrategies>();
                                              ctx.ForStrategy<AlwaysFalse>().Use<AlwaysTrueStrategyImpl>();
                                          });

            Assert.True(container.IsStrategyEnabled(typeof(AlwaysTrueStrategyImpl)));
        }
    }

    [AppSettings(Order = 0, Key = "The key")]
    [AlwaysFalse(Order = 1)]
    public class MyFeatureDisabledWithMultipleStrategies : BaseFeature
    {
    }
}
