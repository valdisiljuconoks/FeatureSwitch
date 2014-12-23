using FeatureSwitch.Strategies;

namespace FeatureSwitch.Tests.Features
{
    [AppSettings(Key = "MySampleFeatureKey")]
    public class MySampleFeature : BaseFeature
    {
        public override string GroupName
        {
            get
            {
                return "Sample group";
            }
        }
    }
}
