using System.Web;
using FeatureSwitch.Tests.Features;
using Moq;
using Xunit;

namespace FeatureSwitch.Tests
{
    public class FeatureSetBuilderWithDiscoveredFeatures
    {
        public FeatureSetBuilderWithDiscoveredFeatures()
        {
            var httpSessionMock = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();

            httpSessionMock.Setup(b => b.Session).Returns(() => session.Object);

            HttpContextFactory.SetCurrentContext(httpSessionMock.Object);
        }

        [Fact]
        public void BuilderTest_2ndLevelInheritance_AddedFeatureFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context => context.AddFeature<MySample2ndLevelFeature>());

            Assert.NotNull(container.GetFeature<MySample2ndLevelFeature>());
        }

        [Fact]
        public void BuilderTest_2ndLevelInheritance_NonAddedFeatureNotFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context => context.AddFeature<MySample2ndLevelFeature>());

            Assert.Null(container.GetFeature<MySample1stLevelFeature>(false));
        }

        [Fact]
        public void BuilderTest_2ndLevelInheritance_AllFeaturesFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context =>
            {
                context.AddFeature<MySample2ndLevelFeature>();
                context.AutoDiscoverFeatures = true;
            });

            Assert.NotNull(container.GetFeature<MySample2ndLevelFeature>());
            Assert.NotNull(container.GetFeature<MySample1stLevelFeature>());
        }

        [Fact]
        public void BuilderTest_BuildContextWithDiscovery_DiscoveredFeatureFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build();

            Assert.NotNull(container.GetFeature<MySampleDiscoveredFeature>());
            Assert.True(container.IsEnabled<MySampleDiscoveredFeature>());
        }
    }

    public class MySample2ndLevelFeature : MySample1stLevelFeature
    {
    }

    public class MySample1stLevelFeature : BaseFeature
    {
    }
}
