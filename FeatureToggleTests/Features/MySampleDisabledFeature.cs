using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Features
{
    [AppSettings(Key = "MySampleDisabledFeatureKey")]
    public class MySampleDisabledFeature : BaseFeature
    {
    }
}