using FeatureSwitch.AspNet.Mvc;
using Owin;

namespace FeatureSwitch.AspNet.Sample
{
    using FeatureSwitch.StructureMap;

    public partial class Startup
    {
        public void ConfigureFeatureSwitch(IAppBuilder app)
        {
            var builder = new FeatureSetBuilder(new StructureMapDependencyContainer());
            builder.Build();

            app.MapFeatureSwitch(); //.WithRoles("Administrators");
        }
    }
}
