using FeatureToggle.AspNet.Mvc5;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("FeatureToggleStartup", typeof(FeatureToggleStartup))]

namespace FeatureToggle.AspNet.Mvc5
{
    public class FeatureToggleStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new FeatureSetBuilder();
            builder.Build().WithRoute("FeatureToggle");
        }
    }
}
