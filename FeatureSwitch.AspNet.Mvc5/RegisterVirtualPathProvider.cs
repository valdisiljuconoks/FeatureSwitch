using System.Web.Hosting;
using FeatureSwitch.AspNet.Mvc;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(RegisterVirtualPathProvider), "PreStart")]

namespace FeatureSwitch.AspNet.Mvc
{
    public static class RegisterVirtualPathProvider
    {
        public static void PreStart()
        {
            /*
             * Virtual path provider registration in very early stage in web server processing pipeline (before registering routes) is necessary.
             * If provider is registered after routes have been added it's skipped and GetFile is not called for ordinary Razor views.
             * Haven't dig into this detail very deep..
             * One of the easiest way to execute some code before Application_Start (high chance that routes are registered somewhere there)
             * is to use WebActivator library.
             */
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyEmbeddedResourceProvider());
        }
    }
}
