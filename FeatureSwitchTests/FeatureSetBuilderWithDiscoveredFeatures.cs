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
        public void BuilderTest_2ndLevelInheritance_DiscoveredFeatureFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(context => context.AddFeature<MySample2ndLevelFeature>());

            Assert.NotNull(container.GetFeature<MySample2ndLevelFeature>());
        }

        [Fact]
        public void BuilderTest_BuildContextWithDiscovery_DiscoveredFeatureFound()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build();

            Assert.NotNull(container.GetFeature<MySampleDiscoveredFeature>());
        }
    }

    public class MySample2ndLevelFeature : MySample1stLevelFeature
    {
    }

    public class MySample1stLevelFeature : BaseFeature
    {
    }
}
