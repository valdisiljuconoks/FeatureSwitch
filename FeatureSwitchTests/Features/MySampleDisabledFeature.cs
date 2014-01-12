using FeatureSwitch.Strategies;

namespace FeatureSwitch.Tests.Features
{
    [AppSettings(Key = "MySampleDisabledFeatureKey")]
    public class MySampleDisabledFeature : BaseFeature
    {
    }
}