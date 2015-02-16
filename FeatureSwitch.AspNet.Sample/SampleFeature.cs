using FeatureSwitch.Strategies;

namespace FeatureSwitch.AspNet.Sample
{
    [AlwaysTrue]
    public class SampleFeature : BaseFeature
    {
    }
    
    [Cookie(Key = "SampleCookie")]
    public class SampleCookieFeature : BaseFeature
    {
    }
}
