using System;
using FeatureSwitch.Strategies.Implementations;

namespace FeatureSwitch.Strategies
{
    public class Cookie : FeatureStrategyAttribute
    {
        public override Type DefaultImplementation => typeof(CookieStrategyImpl);
    }
}
