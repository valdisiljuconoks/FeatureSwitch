﻿using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Owin;

namespace FeatureToggle.AspNet.Mvc5
{
    public static class RouteRegistrationExtensions
    {
        public static void MapFeatureToggle(this IAppBuilder target, string routeName = Const.ModuleName)
        {
            CreateAndInsertRoute(routeName);
        }

        public static void MapFeatureToggleRoute(this RouteCollection target, string routeName = Const.ModuleName)
        {
            CreateAndInsertRoute(routeName);
        }

        public static FeatureSetContainer WithRoute(this FeatureSetContainer target, string routeName)
        {
            CreateAndInsertRoute(routeName);
            return target;
        }

        private static void CreateAndInsertRoute(string routeName)
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

            // need to register assembly resource loader provider
            HostingEnvironment.RegisterVirtualPathProvider(new ResourceProvider());

            // also we need to register custom view engine to convince asp.net to load views from custom location
            ViewEngines.Engines.Add(new CustomViewEngine());
        }
    }
}
