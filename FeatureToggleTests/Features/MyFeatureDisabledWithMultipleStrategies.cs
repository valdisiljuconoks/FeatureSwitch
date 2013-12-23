using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Features
{
    [AppSettingsStrategy(Order = 0)]
    [AlwaysFalseStrategy(Order = 1)]
    public class MyFeatureDisabledWithMultipleStrategies : BaseFeature
    {
    }
}
