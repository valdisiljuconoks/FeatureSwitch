using System.Configuration;

namespace FeatureToggle.Strategies
{
    public class ApplicationSettingsStrategyReader : BaseStrategy
    {
        public override bool Read(ConfigurationContext buildConfigurationContext)
        {
            var value = ConfigurationManager.AppSettings[buildConfigurationContext.Key];
            bool result;
            bool.TryParse(value, out result);
            return result;
        }
    }
}
