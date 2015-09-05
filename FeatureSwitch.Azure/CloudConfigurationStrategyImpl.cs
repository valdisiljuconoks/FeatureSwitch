using FeatureSwitch.Strategies.Implementations;
using Microsoft.Azure;

namespace FeatureSwitch.Azure
{
    public class CloudConfigurationStrategyImpl : BaseStrategyReaderImpl {
        public override bool Read()
        {
            var value = CloudConfigurationManager.GetSetting(Context.Key);
            return ConvertToBoolean(value);
        }
    }
}
