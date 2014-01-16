using System.Web.Routing;

namespace FeatureSwitch.AspNet.Mvc
{
    public static class AuthorizationExtensions
    {
        public static RouteCollection WithRoles(this RouteCollection target, string roles)
        {
            RouteConfiguration.RoleNames = roles;
            return target;
        }
    }
}
