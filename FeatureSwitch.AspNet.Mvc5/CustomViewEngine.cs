using System.Web.Mvc;

namespace FeatureSwitch.AspNet.Mvc
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/" + Const.ModuleName + "/Views/{1}/{0}.cshtml" };
        }


    }
}