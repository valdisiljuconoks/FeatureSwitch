using FeatureToggle.Strategies;

namespace FeatureToggle.AspNet.Sample
{
    [AppSettingsStrategy(Key = "StyleOptimizationDisabled")]
    public class StyleOptimizationDisabled : BaseFeature
    {
    }
}
