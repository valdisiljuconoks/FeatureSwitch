using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FeatureSwitch.AspNet.Sample
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //var builder = new FeatureSetBuilder();
            //builder.Build();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
