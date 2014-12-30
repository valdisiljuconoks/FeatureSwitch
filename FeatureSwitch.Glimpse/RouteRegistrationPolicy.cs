using System.Web.Hosting;
using System.Web.Routing;
using FeatureSwitch.AspNet.Mvc;
using Glimpse.Core.Extensibility;

namespace FeatureSwitch.Glimpse
{
    public class RouteRegistrationPolicy : IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            var route = new Route("FeatureSwitchConfigScript", new AssemblyResourceRouteHandler());
            RouteTable.Routes.Insert(0, route);

            // need to register assembly resource loader provider
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyEmbeddedResourceProvider());
            return RuntimePolicy.On;
        }

        public RuntimeEvent ExecuteOn
        {
            get
            {
                return RuntimeEvent.Initialize;
            }
        }
    }
}
