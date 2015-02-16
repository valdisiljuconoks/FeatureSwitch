using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace FeatureSwitch.AspNet.Mvc
{
    internal class ResourceVirtualFile : VirtualFile
    {
        private static Assembly _assembly;
        private readonly string _fileName;

        public ResourceVirtualFile(string virtualPath) : base(virtualPath)
        {
            _fileName = VirtualPathUtility.ToAppRelative(virtualPath);
            ModuleAssembly = GetType().Assembly;
        }

        public static Assembly ModuleAssembly
        {
            get
            {
                return _assembly ?? (_assembly = typeof(ResourceVirtualFile).Assembly);
            }
            protected set
            {
                _assembly = value;
            }
        }

        public override Stream Open()
        {
            var resource = AssemblyEmbeddedResourceProvider.GetResourceName(_fileName);
            return !string.IsNullOrWhiteSpace(resource) ? ModuleAssembly.GetManifestResourceStream(resource) : null;
        }
    }
}
