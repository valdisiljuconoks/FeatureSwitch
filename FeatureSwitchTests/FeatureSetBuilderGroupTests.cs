using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;
using FeatureSwitch.Tests.Features;
using Xunit;

namespace FeatureSwitch.Tests
{
    public class FeatureSetBuilderGroupTests
    {
        [Fact]
        public void BuilderTest_OneFeatureOneStrategy_Success()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
            {
                ctx.AddFeature<MySampleFeature>();
                ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
            });
        }
    }
}
