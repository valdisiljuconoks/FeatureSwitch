using FeatureToggle.Strategies;

namespace FeatureToggle.AspNet.Sample
{
    [AppSettingsStrategy(Key = "MyFirstFeatureKey")]
    public class MyFirstFeature : BaseFeature
    {
    }
}
