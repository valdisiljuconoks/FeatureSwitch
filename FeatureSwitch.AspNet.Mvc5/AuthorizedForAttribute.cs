using System;

namespace FeatureSwitch.AspNet.Mvc
{
    public class AuthorizedForAttribute : FeatureStrategyAttribute
    {
        public override Type DefaultImplementation
        {
            get { return typeof (AuthorizedForStrategyImpl); }
        }
    }
}
