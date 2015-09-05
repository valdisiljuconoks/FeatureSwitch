using System;

namespace FeatureSwitch.EPiServer.Strategies
{
    public class EPiServerDatabase : FeatureStrategyAttribute
    {
        public override Type DefaultImplementation
        {
            get { return typeof (EPiServerDatabaseStrategyImpl); }
        }
    }
}
