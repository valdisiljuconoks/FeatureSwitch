using System;
using System.Web;

namespace FeatureSwitch.Web.Optimization
{
    public static class Styles
    {
        private static string defaultTagFormat = "<link href=\"{0}\" rel=\"stylesheet\"/>";
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

        private static IHtmlString Render(Type feature, string[] paths)
        {
            return !FeatureContext.IsEnabled(feature) ? System.Web.Optimization.Styles.Render(paths) : BundleRenderer.Render(paths, DefaultTagFormat);
        }
    }
}
