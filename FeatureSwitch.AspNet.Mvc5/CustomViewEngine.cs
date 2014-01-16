using System.Web.Mvc;

namespace FeatureSwitch.AspNet.Mvc
{
    public class CustomViewEngine : WebFormViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/" + Const.ModuleName + "/Views/{1}/{0}.aspx" };
        }
    }
}