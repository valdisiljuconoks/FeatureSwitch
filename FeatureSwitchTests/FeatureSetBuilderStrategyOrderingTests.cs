using System.Configuration;
using FeatureSwitch.Strategies;
using Xunit;

namespace FeatureSwitch.Tests
{
    public class FeatureSetBuilderStrategyOrderingTests
    {
        [Fact]
        public void BuilderTest_FeatureWithStrategiesSameOrder_ValidateThrows()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context => context.AddFeature<FeatureWithSameOrderStrategy>());

            Assert.Throws<ConfigurationErrorsException>(() => container.ValidateConfiguration());
        }

        [Fact]
        public void BuilderTest_FeatureWithStrategiesSameOrder_IsEnabledThrows()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context => context.AddFeature<FeatureWithSameOrderStrategy>());

            Assert.Throws<ConfigurationErrorsException>(() => FeatureContext.IsEnabled<FeatureWithSameOrderStrategy>());
        }

        [AppSettings(Key = "Key1")]
        [AppSettings(Key = "Key2")]
        public class FeatureWithSameOrderStrategy : BaseFeature
        {
        }
    }
}
