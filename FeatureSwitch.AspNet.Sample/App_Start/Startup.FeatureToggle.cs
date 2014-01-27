using FeatureSwitch.AspNet.Mvc;
using Owin;

namespace FeatureSwitch.AspNet.Sample
{
    public partial class Startup
    {
        public void ConfigureFeatureToggle(IAppBuilder app)
        {
            var builder = new FeatureSetBuilder();
            builder.Build();

            app.MapFeatureSwitch(); //.WithRoles("Administrators");
        }
    }
}
