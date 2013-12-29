using System.Configuration;
using FeatureToggle.Strategies;
using Xunit;

namespace FeatureToggle.Tests
{
    public class FeatureSetBuilderStrategyOrderingTests
    {
        [Fact]
        public void BuilderTest_FeatureWithStrategiesSameOrder_ConfigException()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context => context.AddFeature<FeatureWithSameOrderStrategy>());

            Assert.Throws<ConfigurationErrorsException>(() => container.ValidateConfiguration());
        }

        [AppSettingsStrategy(Key = "Key1")]
        [AppSettingsStrategy(Key = "Key2")]
        public class FeatureWithSameOrderStrategy : BaseFeature
        {
        }
    }
}
