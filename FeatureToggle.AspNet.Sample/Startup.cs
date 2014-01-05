using FeatureToggle.AspNet.Sample;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace FeatureToggle.AspNet.Sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureFeatureToggle(app);
        }
    }
}
