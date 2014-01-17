using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using FeatureSwitch.AspNet.Mvc;

namespace FeatureSwitch.EPiServer
{
    [InitializableModule]
    public class FeatureToggleInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var builder = new FeatureSetBuilder();
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
