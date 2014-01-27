using FeatureSwitch.Strategies;

namespace FeatureSwitch.Web.Optimization
{
    [QueryString(Key = "DisableStyleOptimization")]
    [HttpSession(Key = "FeatureSwitch_DisableStyleOptimization", Order = 1)]
    public class DisableStyleOptimization : BaseFeature
    {
    }
}
