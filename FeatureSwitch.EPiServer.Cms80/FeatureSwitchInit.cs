﻿using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using FeatureSwitch.AspNet.Mvc;
using FeatureSwitch.StructureMap;

namespace FeatureSwitch.EPiServer
{
    [InitializableModule]
    [ModuleDependency(typeof (ServiceContainerInitialization))]
    public class FeatureSwitchInit : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var builder = new FeatureSetBuilder(new StructureMapDependencyContainer(context.Container));
            builder.Build(ctx => { ctx.AutoDiscoverFeatures = true; })
                .WithRoute("modules/FeatureSwitch")
                .WithRoles("Administrators, WebAdmins, CmsAdmins")
                .ValidateConfiguration();
        }

        public void Initialize(InitializationEngine context) {}

        public void Uninitialize(InitializationEngine context) {}
    }
}