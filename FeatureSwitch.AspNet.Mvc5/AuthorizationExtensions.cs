using System.Web.Routing;

namespace FeatureSwitch.AspNet.Mvc
{
    public static class AuthorizationExtensions
    {
        public static FeatureSetContainer WithRoles(this FeatureSetContainer target, string roles)
        {
            RouteConfiguration.RoleNames = roles;
            return target;
        }

        public static RouteCollection WithRoles(this RouteCollection target, string roles)
        {
            RouteConfiguration.RoleNames = roles;
            return target;
        }
    }
}
