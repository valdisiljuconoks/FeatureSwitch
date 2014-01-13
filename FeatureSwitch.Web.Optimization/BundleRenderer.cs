using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace FeatureSwitch.Web.Optimization
{
    internal class BundleRenderer
    {
        public static IHtmlString Render(string[] paths, string defaultTagFormat)
        {
            var list = paths.SelectMany(virtualPath => BundleResolver.Current.GetBundleContents(virtualPath)).ToList();

            var stringBuilder = new StringBuilder();
            foreach (var path in list)
            {
                stringBuilder.Append(string.Format(defaultTagFormat, HttpUtility.UrlPathEncode(VirtualPathUtility.ToAbsolute(path))));
                stringBuilder.Append(Environment.NewLine);
            }

            return new HtmlString(stringBuilder.ToString());
        }
    }
}
