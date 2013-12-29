using System.Configuration;

namespace FeatureToggle.Strategies.Implementations
{
    public class AppSettingsStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            var value = ConfigurationManager.AppSettings[Context.Key];
            return ConvertToBoolean(value);
        }
    }
}
