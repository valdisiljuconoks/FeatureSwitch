using FeatureToggle.Strategies;
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
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MyFeatureDisabledWithMultipleStrategies>();
                              ctx.ForStrategy<AppSettingsStrategy>().Use<AlwaysFalseStrategyImpl>();
                          });

            var feature = FeatureContext.GetFeature<MyFeatureDisabledWithMultipleStrategies>();

            Assert.False(feature.IsEnabled);
        }
    }

    [AppSettingsStrategy(Order = 0)]
    [AlwaysFalseStrategy(Order = 1)]
    public class MyFeatureDisabledWithMultipleStrategies : BaseFeature
    {
    }
}
