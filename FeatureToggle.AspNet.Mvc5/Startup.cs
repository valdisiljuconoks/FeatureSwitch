using FeatureToggle.AspNet.Mvc5;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("FeatureToggleStartup", typeof(Startup))]

namespace FeatureToggle.AspNet.Mvc5
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new FeatureSetBuilder();
            builder.Build();
        }
    }
}
