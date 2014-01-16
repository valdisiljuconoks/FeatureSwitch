using Owin;

namespace FeatureSwitch.AspNet.Mvc
{
    public static class AuthorizationExtensionsOwin
    {
        public static IAppBuilder WithRoles(this IAppBuilder target, string roles)
        {
            RouteConfiguration.RoleNames = roles;
            return target;
        }
    }
}
