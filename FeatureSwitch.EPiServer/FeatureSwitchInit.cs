using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using FeatureSwitch.AspNet.Mvc;
using FeatureSwitch.StructureMap;

namespace FeatureSwitch.EPiServer
{
    [InitializableModule]
    public class FeatureToggleInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var builder = new FeatureSetBuilder(new StructureMapDependencyContainer());
            builder.Build()
                   .WithRoute("modules/FeatureSwitch")
                   .WithRoles("Administrators")
                   .ValidateConfiguration();
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}
