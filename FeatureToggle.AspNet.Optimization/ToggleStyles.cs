using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace FeatureToggle.AspNet.Optimization
{
    public static class ToggleStyles
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
            if (!FeatureContext.IsEnabled(feature))
            {
                return Styles.Render(paths);
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
