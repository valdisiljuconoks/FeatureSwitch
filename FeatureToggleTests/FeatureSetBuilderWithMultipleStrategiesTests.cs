using FeatureToggle.Strategies;
using FeatureToggle.Strategies.Implementations;
using StructureMap;
using Xunit;

namespace FeatureToggle.Tests
{
    public class FeatureSetBuilderWithMultipleStrategiesTests
    {
        [Fact]
        public void BuilderTest_MultipleStrategies_FeatureEnabled()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MyFeatureDisabledWithMultipleStrategies>();
                                              ctx.ForStrategy<AppSettings>().Use<AlwaysFalseStrategyImpl>();
                                          });

            Assert.False(container.IsEnabled<MyFeatureDisabledWithMultipleStrategies>());
        }
    }

    [AppSettings(Order = 0)]
    [AlwaysFalse(Order = 1)]
    public class MyFeatureDisabledWithMultipleStrategies : BaseFeature
    {
    }
}
