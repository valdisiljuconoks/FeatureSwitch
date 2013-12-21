using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Features
{
    [AppSettingsStrategy(Key = "MySampleFeatureKey")]
    public class MySampleFeature : BaseFeature
    {
    }
}
