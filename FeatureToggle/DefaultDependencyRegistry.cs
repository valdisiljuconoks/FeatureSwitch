using FeatureToggle.Strategies;
using FeatureToggle.Strategies.Implementations;
using StructureMap.Configuration.DSL;

namespace FeatureToggle
{
    public class DefaultDependencyRegistry : Registry
    {
        public DefaultDependencyRegistry()
        {
            For<IAppSettingsReader>().Use<AppSettingsStrategyImpl>();
        }
    }
}
