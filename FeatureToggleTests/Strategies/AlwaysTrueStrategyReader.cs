using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Strategies
{
    public class AlwaysTrueStrategyReader : IStrategyStorageReader
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
