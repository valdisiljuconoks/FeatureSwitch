using Owin;
using System.Web.Routing;
namespace FeatureToggle.AspNet.Mvc5
{
    public static class AuthorizationExtensions
    {
        public static IAppBuilder WithRoles(this IAppBuilder target, string roles)
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
