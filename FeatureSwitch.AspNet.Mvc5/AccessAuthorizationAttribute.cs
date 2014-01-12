using System;
using System.Web;
using System.Web.Mvc;

namespace FeatureSwitch.AspNet.Mvc5
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AccessAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Roles = RouteConfiguration.RoleNames;
            return base.AuthorizeCore(httpContext);
        }
    }
}
