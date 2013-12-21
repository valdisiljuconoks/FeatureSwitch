using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Strategies
{
    public class UnitTestAppSettingsStrategyReader : IAppSettingsReader
    {
        public bool Read(ConfigurationContext buildConfigurationContext)
        {
            return true;
        }

        public void Initialize()
        {
        }
    }
}
