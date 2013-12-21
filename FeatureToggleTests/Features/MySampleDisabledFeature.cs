using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Features
{
    [AppSettingsStrategy(Key = "MySampleDisabledFeatureKey")]
    public class MySampleDisabledFeature : BaseFeature
    {
    }
}