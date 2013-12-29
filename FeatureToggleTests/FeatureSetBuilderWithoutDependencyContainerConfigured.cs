using FeatureToggle.Tests.Features;
using Xunit;

namespace FeatureToggle.Tests
{
    public class FeatureSetBuilderWithoutDependencyContainerConfigured
    {
        [Fact]
        public void BuilderTest_NoDependencyContainerDisabledFeature_UsesDefaultImplementations()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx => ctx.AddFeature<MySampleDisabledFeature>());

            var isEnabled = container.IsEnabled<MySampleDisabledFeature>();

            Assert.False(isEnabled);
        }

        [Fact]
        public void BuilderTest_NoDependencyContainer_UsesDefaultImplementations()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx => ctx.AddFeature<MySampleFeature>());

            var isEnabled = container.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }
        
        [Fact]
        public void BuilderTest_NoDependencyContainerFeatureWithNoKey_UsesDefaultImplementations()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx => ctx.AddFeature<MySampleNonExistingKeyFeature>());

            var isEnabled = container.IsEnabled<MySampleNonExistingKeyFeature>();

            Assert.False(isEnabled);
        }
    }
}
