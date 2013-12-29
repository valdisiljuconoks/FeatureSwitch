using FeatureToggle.Strategies;
using FeatureToggle.Strategies.Implementations;

namespace FeatureToggle.Tests.Strategies
{
    public class StrategyWithConstructorParameterReader : BaseStrategyReaderImpl
    {
        private readonly ISampleInjectedInterface sample;

        public StrategyWithConstructorParameterReader(ISampleInjectedInterface sample)
        {
            this.sample = sample;
            sample.CheckNull("sample");
        }

        public object Dependency
        {
            get
            {
                return this.sample;
            }
        }

        public override bool Read()
        {
            return true;
        }
    }
}
