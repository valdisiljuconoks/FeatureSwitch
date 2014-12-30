using System.Web;
using System.Web.Routing;

namespace FeatureSwitch.Glimpse
{
    public class AssemblyResourceRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new AssemblyResourceJavascriptHandler(requestContext);
        }
    }
}
