using FeatureSwitch;
using FeatureSwitch.Strategies;

namespace EpiSample
{
    [HttpSession(Key = "Feature1_State")]
    public class Feature1 : BaseFeature { }
}
