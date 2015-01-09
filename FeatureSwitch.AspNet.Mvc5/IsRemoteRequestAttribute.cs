using System;

namespace FeatureSwitch.AspNet.Mvc
{
    public class IsRemoteRequestAttribute : FeatureStrategyAttribute
    {
        public IsRemoteRequestAttribute()
        {
            Key = "953BB6CA-F33B-469C-BDA2-1C55DFE21CE1";
        }

        public override Type DefaultImplementation
        {
            get
            {
                return typeof(IsRemotelRequestStrategyImpl);
            }
        }
    }
}
