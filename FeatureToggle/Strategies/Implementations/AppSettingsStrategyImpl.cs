using System.Configuration;

namespace FeatureToggle.Strategies.Implementations
{
    public class AppSettingsStrategyImpl : BaseStrategyReaderImpl, IAppSettingsReader
    {
        public override bool Read()
        {
            var value = ConfigurationManager.AppSettings[Context.Key];
            return ConvertToBoolean(value);
        }
    }
}
