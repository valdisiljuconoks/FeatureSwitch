using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System;

namespace FeatureToggle.EPiServer
{
    [InitializableModule]
    public class FeatureToggleInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var builder = new FeatureSetBuilder();
            builder.Build()
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
