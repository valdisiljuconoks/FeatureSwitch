using System.Web;
using FeatureToggle.Strategies;
using Moq;
using Xunit;

namespace FeatureToggle.Tests.StrategyTests
{
    public class HttpSessionStrategyTests
    {
        public HttpSessionStrategyTests()
        {
            var httpSessionMock = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();

            httpSessionMock.Setup(b => b.Session).Returns(() => session.Object);

            HttpContextFactory.SetCurrentContext(httpSessionMock.Object);
        }

        [Fact]
        public void TestHttpSession_Success()
        {
            var builder = new FeatureSetBuilder();

            var container = builder.Build(context => context.AddFeature<FeatureWithSessionStorage>());

            Assert.False(container.IsEnabled<FeatureWithSessionStorage>());
        }
    }

    [HttpSession(Key = "SampleKey")]
    public class FeatureWithSessionStorage : BaseFeature
    {
    }
}
