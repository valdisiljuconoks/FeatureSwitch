using System;

namespace FeatureSwitch.Azure
{
    public class CloudConfigurationAttribute : FeatureStrategyAttribute
    {
        public override Type DefaultImplementation
        {
            get { return typeof (CloudConfigurationStrategyImpl); }
        }
    }
}
