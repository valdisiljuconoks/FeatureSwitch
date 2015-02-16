using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace FeatureSwitch.AspNet.Mvc
{
    public class AssemblyEmbeddedResourceProvider : VirtualPathProvider
    {
        private static readonly List<string> resources = typeof(AssemblyEmbeddedResourceProvider).Assembly.GetManifestResourceNames().ToList();

        public override bool FileExists(string virtualPath)
        {
            return ShouldHandle(virtualPath) || base.FileExists(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return ShouldHandle(virtualPath)
                       ? null //new CacheDependency((string)null)
                       : base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return ShouldHandle(virtualPath)
                       ? new ResourceVirtualFile(virtualPath)
                       : base.GetFile(virtualPath);
        }

        internal static string GetResourceName(string virtualPath)
        {
            return Const.NamespaceName + "." + TranslateToResource(virtualPath).ToLower();
        }

        private static string TranslateToResource(string url)
        {
            if (url.StartsWith("~"))
            {
                url = url.Substring(1);
            }

            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            return url.Replace('/', '.');
        }

        private static bool ShouldHandle(string virtualPath)
        {
            var doesUrlContain = VirtualPathUtility.ToAppRelative(virtualPath).ToLower().Contains(Const.ModuleName.ToLower());
            var doesResourcesContain = resources.Any(r =>
                                                     r.Equals(GetResourceName(virtualPath),
                                                              StringComparison.InvariantCultureIgnoreCase));

            return doesUrlContain && doesResourcesContain;
        }
    }
}
