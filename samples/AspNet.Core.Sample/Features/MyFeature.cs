using FeatureSwitch;
using FeatureSwitch.Strategies;

namespace AspNet.Core.Sample.Features
{
    [QueryString(Key="myfeature")]
    public class MyFeature : BaseFeature { }
}
