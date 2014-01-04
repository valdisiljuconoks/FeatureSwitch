using FeatureToggle.Strategies;
using Moq;
using System;
using System.Collections.Specialized;
using System.Web;
using Xunit;
using Xunit.Extensions;

namespace FeatureToggle.Tests.StrategyTests
{
    public class QueryStringStrategyTests
    {
        public QueryStringStrategyTests()
        {
            var httpSessionMock = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();

            var queryStringKeyValueCollection = new NameValueCollection
            {
                { "id", "1" },
                { "SampleKey", "true" },
                { "AnotherKey", "false" }
            };

            request.SetupGet(r => r.QueryString).Returns(queryStringKeyValueCollection);
            httpSessionMock.Setup(b => b.Request).Returns(() => request.Object);

            HttpContextFactory.SetCurrentContext(httpSessionMock.Object);
        }

        [Theory]
        [InlineData(typeof(FeatureFromQueryString), true)]
        [InlineData(typeof(AnotherFeatureFromQueryString), false)]
        public void TestQueryStringStrategy_Success(Type feature, bool expectedState)
        {
            var builder = new FeatureSetBuilder();

            var container = builder.Build(context => context.AddFeature(feature));

            Assert.True(container.IsEnabled(feature) == expectedState);
        }
    }

    [QueryString(Key = "SampleKey")]
    public class FeatureFromQueryString : BaseFeature
    {
    }

    [QueryString(Key = "AnotherKey")]
    public class AnotherFeatureFromQueryString : BaseFeature
    {
    }
}
