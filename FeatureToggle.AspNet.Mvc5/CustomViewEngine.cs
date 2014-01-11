using System.Web.Mvc;

namespace FeatureToggle.AspNet.Mvc5
{
    public class CustomViewEngine : WebFormViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/" + Const.ModuleName + "/Views/{1}/{0}.aspx" };
        }
    }
}