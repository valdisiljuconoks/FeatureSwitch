using FeatureSwitch.Tests.Strategies;

namespace FeatureSwitch.Tests.Features
{
    [WritableHashMapStrategy(Key = "WritableHash1")]
    [WritableHashMapStrategy2(Key = "WritableHash2", Order = 1)]
    public class MySampleFeatureWithMultipleWritableStrategies : BaseFeature
    {
    }
}
