using System.IO;
using System.Web;
using System.Web.Hosting;

namespace FeatureSwitch.AspNet.Mvc
{
    internal class ResourceVirtualFile : VirtualFile
    {
        private readonly string _fileName;

        public ResourceVirtualFile(string virtualPath) : base(virtualPath)
        {
            _fileName = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            return GetType().Assembly.GetManifestResourceStream(Const.NamespaceName + "." + AssemblyEmbeddedResourceProvider.TranslateToResource(_fileName));
        }
    }
}
