using System.Collections.Generic;

namespace FeatureToggle.AspNet.Mvc5
{
    public class FeatureToggleViewModel
    {
        public IList<BaseFeature> Features { get; set; }

        public string RouteName { get; set; }
    }
}
