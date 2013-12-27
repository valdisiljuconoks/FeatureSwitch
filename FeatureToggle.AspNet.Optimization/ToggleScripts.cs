using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace FeatureToggle.AspNet.Optimization
{
    public static class ToggleScripts
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

        public static IHtmlString Render(Type feature, params string[] paths)
        {
            if (!FeatureContext.IsEnabled(feature))
            {
                return Scripts.Render(paths);
            }

            var list = paths.SelectMany(virtualPath => BundleResolver.Current.GetBundleContents(virtualPath)).ToList();

            var stringBuilder = new StringBuilder();
            foreach (var path in list)
            {
                stringBuilder.Append(string.Format(DefaultTagFormat, HttpUtility.UrlPathEncode(VirtualPathUtility.ToAbsolute(path))));
                stringBuilder.Append(Environment.NewLine);
            }

            return new HtmlString(stringBuilder.ToString());
        }
    }
}
