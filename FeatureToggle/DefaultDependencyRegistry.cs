using FeatureToggle.Strategies;
using StructureMap.Configuration.DSL;

namespace FeatureToggle
{
    public class DefaultDependencyRegistry : Registry
    {
        public DefaultDependencyRegistry()
        {
            For<IAppSettingsReader>().Use<ApplicationSettingsStrategyImpl>();
        }
    }
}
