using FeatureSwitch.Strategies;

namespace FeatureSwitch.Web.Optimization
{
    [QueryString(Key = "DisableScriptOptimization")]
    [HttpSession(Key = "FeatureSwitch_DisableScriptOptimization", Order = 1)]
    public class DisableScriptOptimization : BaseFeature
    {
    }
}
