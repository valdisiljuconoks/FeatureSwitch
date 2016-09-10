using System;
using FeatureSwitch.Strategies.Implementations;

namespace FeatureSwitch.Strategies
{
    public class AlwaysTrue : FeatureStrategyAttribute
    {
        public AlwaysTrue()
        {
            Key = "C6A5582C-4E1B-4E01-913C-E68307AE9098";
        }

        public override Type DefaultImplementation
        {
            get
            {
                return typeof(AlwaysTrueStrategyImpl);
            }
        }
    }
}
