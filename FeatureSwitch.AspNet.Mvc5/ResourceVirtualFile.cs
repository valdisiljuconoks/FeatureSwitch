using System.IO;
using System.Web;
using System.Web.Hosting;

namespace FeatureSwitch.AspNet.Mvc5
{
    internal class ResourceVirtualFile : VirtualFile
    {
        private readonly string fileName;

        public ResourceVirtualFile(string virtualPath) : base(virtualPath)
        {
            this.fileName = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            return GetType().Assembly.GetManifestResourceStream(Const.NamespaceName + "." + AssemblyEmbeddedResourceProvider.TranslateToResource(this.fileName));
        }
    }
}