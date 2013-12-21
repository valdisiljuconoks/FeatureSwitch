using FeatureToggle.Strategies;
using FeatureToggle.Tests.Strategies;
using StructureMap.Configuration.DSL;

namespace FeatureToggle.Tests
{
    public class UnitTestDependencyRegistry : Registry
    {
        public UnitTestDependencyRegistry()
        {
            For<IAppSettingsReader>().Use<UnitTestAppSettingsStrategyReader>();
        }
    }
}
