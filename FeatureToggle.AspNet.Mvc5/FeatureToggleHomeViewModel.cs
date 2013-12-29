using System.Collections.Generic;

namespace FeatureToggle.AspNet.Mvc5
{
    public class FeatureToggleHomeViewModel
    {
        public IList<BaseFeature> Features { get; set; }
    }
}
