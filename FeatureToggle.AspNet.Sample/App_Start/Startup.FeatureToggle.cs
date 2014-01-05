using FeatureToggle.AspNet.Mvc5;
using Owin;

namespace FeatureToggle.AspNet.Sample
{
    public partial class Startup
    {
        public void ConfigureFeatureToggle(IAppBuilder app)
        {
            var builder = new FeatureSetBuilder();
            builder.Build();

            app.MapFeatureToggle();
        }
    }
}