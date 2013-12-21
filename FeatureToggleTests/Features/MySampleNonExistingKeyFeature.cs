using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Features
{
    [AppSettingsStrategy(Key = "FBC51793-D8EF-4C13-9D9A-988257DD89B1")]
    public class MySampleNonExistingKeyFeature : BaseFeature
    {
    }
}