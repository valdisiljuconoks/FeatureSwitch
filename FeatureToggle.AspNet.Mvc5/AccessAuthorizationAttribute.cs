using System;
using System.Web;
using System.Web.Mvc;

namespace FeatureToggle.AspNet.Mvc5
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AccessAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            base.Roles = RouteConfiguration.RoleNames;
            return base.AuthorizeCore(httpContext);
        }
    }
}
