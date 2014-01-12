using FeatureSwitch.Strategies;

namespace FeatureSwitch.AspNet.Sample
{
    [AppSettings(Key = "ScriptOptimizationDisabled", Order = 0)]
    [HttpSession(Key = "ScriptOptimizationDisabled", Order = 1)]
    public class FeatureScriptOptimizationDisabled : BaseFeature
    {
    }
}
