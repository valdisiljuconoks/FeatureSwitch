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
            return RenderFormat<T>(DefaultTagFormat, paths);
        }

        public static IHtmlString RenderFormat<T>(string tagFormat, params string[] paths) where T : BaseFeature
        {
            return Render(typeof(T), tagFormat, paths);
        }

        private static IHtmlString Render(Type feature, string tagFormat, string[] paths)
        {
            return !FeatureContext.IsEnabled(feature) ? System.Web.Optimization.Styles.Render(paths) : BundleRenderer.Render(paths, tagFormat);
        }
    }
}
