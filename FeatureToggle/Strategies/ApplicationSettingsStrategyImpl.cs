using System.Configuration;

namespace FeatureToggle.Strategies
{
    public class ApplicationSettingsStrategyImpl : BaseStrategyReaderImpl, IAppSettingsReader
    {
        public override bool Read()
        {
            var value = ConfigurationManager.AppSettings[Context.Key];
            bool result;
            bool.TryParse(value, out result);
            return result;
        }
    }
}
