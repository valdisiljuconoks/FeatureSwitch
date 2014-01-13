using System;
using System.Web;

namespace FeatureSwitch.Web.Optimization
{
    public static class Scripts
    {
        private static string defaultTagFormat = "<script src=\"{0}\"></script>";
        public static string DefaultTagFormat
        {
            get
            {
                return defaultTagFormat;
            }
            set
            {
                defaultTagFormat = value;
            }
        }

        public static IHtmlString Render<T>(params string[] paths) where T : BaseFeature
        {
            return Render(typeof(T), paths);
        }

        private static IHtmlString Render(Type feature, params string[] paths)
        {
            return !FeatureContext.IsEnabled(feature) ? System.Web.Optimization.Scripts.Render(paths) : BundleRenderer.Render(paths, DefaultTagFormat);
        }
    }
}
