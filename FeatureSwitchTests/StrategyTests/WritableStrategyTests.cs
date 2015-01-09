using FeatureSwitch.Tests.Features;
using FeatureSwitch.Tests.Strategies;
using Xunit;

namespace FeatureSwitch.Tests.StrategyTests
{
    public class WritableStrategyTests
    {
        [Fact]
        public void ChangeState_AllWritableStrategiesSynced()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
            {
                ctx.AddFeature<MySampleFeatureWithMultipleWritableStrategies>();

                ctx.ForStrategy<WritableHashMapStrategyAttribute>().Use<WritableHashtableStrategy>();
                ctx.ForStrategy<WritableHashMapStrategy2Attribute>().Use<WritableHashtable2Strategy>();
            });

            FeatureContext.Enable<MySampleFeatureWithMultipleWritableStrategies>();

            Assert.True(FeatureContext.IsEnabled<MySampleFeatureWithMultipleWritableStrategies>());

            // TODO:
            //Assert.True(container.IsEnabled<MySampleFeatureWithMultipleWritableStrategies>(typeof(WritableHashtableStrategy)));
            //Assert.True(container.IsEnabled<MySampleFeatureWithMultipleWritableStrategies>(typeof(WritableHashtable2Strategy)));

        }
    }
}
