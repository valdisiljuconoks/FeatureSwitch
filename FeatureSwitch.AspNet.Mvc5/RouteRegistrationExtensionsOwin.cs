using Owin;

namespace FeatureSwitch.AspNet.Mvc
{
    public static class RouteRegistrationExtensionsOwin
    {
        public static IAppBuilder MapFeatureSwitch(this IAppBuilder target, string routeName = Const.ModuleName)
        {
            RouteRegistrationExtensions.CreateAndInsertRoute(routeName);
            return target;
        }
    }
}
