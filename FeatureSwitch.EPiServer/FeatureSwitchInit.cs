using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using FeatureSwitch.AspNet.Mvc;
using FeatureSwitch.EPiServer.Strategies;
using FeatureSwitch.StructureMap;

namespace FeatureSwitch.EPiServer
{
    [InitializableModule]
    public class FeatureSwitchInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var builder = new FeatureSetBuilder(new StructureMapDependencyContainer());
            builder.Build(ctx =>
            {
                ctx.ForStrategy<EPiServerDatabase>().Use<EPiServerDatabaseStrategyImpl>();
                ctx.AutoDiscoverFeatures = true;
            })
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
