using Microsoft.Extensions.Configuration;

namespace FeatureSwitch.Strategies.Implementations
{
    public class AppSettingsStrategyImpl : BaseStrategyReaderImpl
    {
        private readonly IConfiguration _config;

        public AppSettingsStrategyImpl(IConfiguration config)
        {
            _config = config;
        }

        public override bool Read()
        {
            return ConvertToBoolean(_config[Context.Key]);
        }
    }
}
