using System;

namespace FeatureSwitch.EPiServer.Strategies
{
    public class EPiServerDatabaseAttribute : FeatureStrategyAttribute
    {
        public override Type DefaultImplementation
        {
            get { return typeof (EPiServerDatabaseStrategyImpl); }
        }
    }
}
