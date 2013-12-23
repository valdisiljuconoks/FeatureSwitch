using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Strategies
{
    public class UnitTestAppSettingsStrategyReader : IAppSettingsReader
    {
        public bool Read()
        {
            return true;
        }

        public void Initialize(ConfigurationContext configurationContext)
        {
        }
    }
}
