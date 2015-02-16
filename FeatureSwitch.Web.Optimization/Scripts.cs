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

        public static IHtmlString RenderIf<T>(params string[] paths) where T : BaseFeature
        {
            return RenderIfFormat<T>(DefaultTagFormat, paths);
        }

        public static IHtmlString RenderIfFormat<T>(string tagFromat, params string[] paths) where T : BaseFeature
        {
            return FeatureContext.IsEnabled(typeof(T)) ? Render(typeof(T), tagFromat, paths) : new HtmlString(string.Empty);
        }

        public static IHtmlString Render<T>(params string[] paths) where T : BaseFeature
        {
            return RenderFormat<T>(DefaultTagFormat, paths);
        }

        public static IHtmlString RenderFormat<T>(string tagFromat, params string[] paths) where T : BaseFeature
        {
            return Render(typeof(T), tagFromat, paths);
        }

        private static IHtmlString Render(Type feature, string tagFromat, params string[] paths)
        {
            return !FeatureContext.IsEnabled(feature) ? System.Web.Optimization.Scripts.RenderFormat(tagFromat, paths) : BundleRenderer.Render(paths, tagFromat);
        }
    }
}
