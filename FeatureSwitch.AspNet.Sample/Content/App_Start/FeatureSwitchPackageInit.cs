using System;
using System.Web.Hosting;
using FeatureSwitch.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof(FeatureSwitch.AspNet.Sample.App_Start.FeatureSwitchPackageInit), "PreStart")]

namespace FeatureSwitch.AspNet.Sample.App_Start {
    public static class FeatureSwitchPackageInit {
        public static void PreStart() {
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyEmbeddedResourceProvider());
        }
    }
}