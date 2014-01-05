using FeatureToggle.Strategies;

namespace FeatureToggle.AspNet.Sample
{
    [AppSettings(Key = "StyleOptimizationDisabled")]
    [QueryString(Key = "DisableStyleOptimization", Order = 1)]
    public class StyleOptimizationDisabled : BaseFeature
    {
    }
}
