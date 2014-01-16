using System.Collections.Generic;

namespace FeatureSwitch.AspNet.Mvc
{
    public class FeatureSwitchViewModel
    {
        public IList<BaseFeature> Features { get; set; }

        public string RouteName { get; set; }
    }
}
