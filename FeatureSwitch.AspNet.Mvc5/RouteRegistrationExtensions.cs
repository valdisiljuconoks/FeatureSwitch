using System.Web.Mvc;
using System.Web.Routing;

namespace FeatureSwitch.AspNet.Mvc
{
    public static class RouteRegistrationExtensions
    {
        public static RouteCollection MapFeatureSwitch(this RouteCollection target, string routeName = Const.ModuleName)
        {
            CreateAndInsertRoute(routeName);
            return target;
        }

        public static FeatureSetContainer WithRoute(this FeatureSetContainer target, string routeName)
        {
            CreateAndInsertRoute(routeName);
            return target;
        }

        internal static void CreateAndInsertRoute(string routeName)
        {
            routeName.CheckNull("routeName");

            var route = new Route(routeName + "/{action}/{param}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new { controller = Const.ModuleName, action = "Index", param = UrlParameter.Optional }),
                DataTokens = new RouteValueDictionary()
            };

            route.DataTokens["Namespaces"] = new[] { Const.NamespaceName };

            RouteTable.Routes.Insert(0, route);
            RouteConfiguration.RouteName = routeName;
        }
    }
}
