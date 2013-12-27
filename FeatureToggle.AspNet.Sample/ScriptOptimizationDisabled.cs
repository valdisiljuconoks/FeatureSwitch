using FeatureToggle.Strategies;

namespace FeatureToggle.AspNet.Sample
{
    [AppSettingsStrategy(Key = "ScriptOptimizationDisabled")]
    public class ScriptOptimizationDisabled : BaseFeature
    {
    }
}
