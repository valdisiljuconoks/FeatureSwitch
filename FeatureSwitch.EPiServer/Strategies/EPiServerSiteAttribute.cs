using System;

namespace FeatureSwitch.EPiServer.Strategies
{
    public class EPiServerSiteAttribute : FeatureStrategyAttribute
    {
        public override Type DefaultImplementation
        {
            get { return typeof (EPiServerSiteStrategyImpl); }
        }
    }
}
