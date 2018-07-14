using System.Web.Mvc;
using EpiSample.Business.Rendering;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace EpiSample.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.Add<IContentRenderer, ErrorHandlingContentRenderer>(ServiceInstanceScope.Transient);
            context.Services.Add<ContentAreaRenderer, AlloyContentAreaRenderer>(ServiceInstanceScope.Transient);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.StructureMap()));
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}