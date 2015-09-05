using System;

namespace FeatureSwitch.EPiServer.Strategies
{
    public class EPiServerSiteAttribute : FeatureStrategyAttribute
    {
        public string Site { get; set; }

        public override Type DefaultImplementation
        {
            get { return typeof (EPiServerSiteStrategyImpl); }
        }
    }
}
