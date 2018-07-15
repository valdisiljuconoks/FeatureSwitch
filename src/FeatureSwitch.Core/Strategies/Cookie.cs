using System;
using FeatureSwitch.Core.Strategies.Implementations;

namespace FeatureSwitch.Core.Strategies
{
    public class Cookie : FeatureStrategyAttribute
    {
        public override Type DefaultImplementation => typeof(CookieStrategyImpl);
    }
}
