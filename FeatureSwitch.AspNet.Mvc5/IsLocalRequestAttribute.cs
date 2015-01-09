using System;

namespace FeatureSwitch.AspNet.Mvc
{
    public class IsLocalRequestAttribute : FeatureStrategyAttribute
    {
        public IsLocalRequestAttribute()
        {
            Key = "6B7743DF-11F2-4D22-A171-383D98A2EDCF";
        }

        public override Type DefaultImplementation
        {
            get
            {
                return typeof(IsLocalRequestStrategyImpl);
            }
        }
    }
}
