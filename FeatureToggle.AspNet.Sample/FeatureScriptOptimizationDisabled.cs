using FeatureToggle.Strategies;

namespace FeatureToggle.AspNet.Sample
{
    [AppSettings(Key = "ScriptOptimizationDisabled", Order = 0)]
    [HttpSession(Key = "ScriptOptimizationDisabled", Order = 1)]
    public class FeatureScriptOptimizationDisabled : BaseFeature
    {
    }
}
