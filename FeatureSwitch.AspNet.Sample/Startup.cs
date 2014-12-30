using FeatureSwitch.AspNet.Sample;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace FeatureSwitch.AspNet.Sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureFeatureSwitch(app);
        }
    }
}
