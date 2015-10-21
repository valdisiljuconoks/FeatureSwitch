using System;
using System.Web;

namespace FeatureSwitch.Web.Optimization
{
    public static class Scripts
    {
        private static string defaultTagFormat = "<script src=\"{0}\"></script>";

        public static string DefaultTagFormat
        {
            get { return defaultTagFormat; }
            set { defaultTagFormat = value; }
        }

        public static IHtmlString RenderIf<T>(params string[] paths) where T : BaseFeature
        {
            return RenderIfFormat<T>(DefaultTagFormat, paths);
        }

        public static IHtmlString RenderIfFormat<T>(string tagFormat, params string[] paths) where T : BaseFeature
        {
            return FeatureContext.IsEnabled(typeof (T)) ? Render(typeof (T), tagFormat, paths) : new HtmlString(string.Empty);
        }

        public static IHtmlString Render<T>(params string[] paths) where T : BaseFeature
        {
            return RenderFormat<T>(DefaultTagFormat, paths);
        }

        public static IHtmlString Render(params string[] paths)
        {
            return RenderFormat(DefaultTagFormat, paths);
        }

        public static IHtmlString RenderFormat(string tagFormat, params string[] paths)
        {
            return System.Web.Optimization.Scripts.RenderFormat(tagFormat, paths);
        }

        public static IHtmlString RenderFormat<T>(string tagFormat, params string[] paths) where T : BaseFeature
        {
            return Render(typeof (T), tagFormat, paths);
        }

        private static IHtmlString Render(Type feature, string tagFormat, params string[] paths)
        {
            return !FeatureContext.IsEnabled(feature) ? System.Web.Optimization.Scripts.RenderFormat(tagFormat, paths) : BundleRenderer.Render(paths, tagFormat);
        }
    }
}
