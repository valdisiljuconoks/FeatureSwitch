using FeatureSwitch.AspNet.Mvc5;
using Owin;

namespace FeatureSwitch.AspNet.Sample
{
    public partial class Startup
    {
        public void ConfigureFeatureToggle(IAppBuilder app)
        {
            var builder = new FeatureSetBuilder();
            builder.Build();

            app.MapFeatureToggle(); //.WithRoles("Administrators");
        }
    }
}
