using FeatureSwitch.AspNet.Mvc;
using Owin;

namespace FeatureSwitch.AspNet.Sample
{
    using FeatureSwitch.StructureMap;

    public partial class Startup
    {
        public void ConfigureFeatureToggle(IAppBuilder app)
        {
            var diContainer = new StructureMapDependencyContainer();
            var builder = new FeatureSetBuilder(diContainer);
            builder.Build();

            app.MapFeatureSwitch(); //.WithRoles("Administrators");
        }
    }
}
