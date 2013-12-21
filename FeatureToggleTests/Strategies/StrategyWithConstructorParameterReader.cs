using FeatureToggle.Strategies;

namespace FeatureToggle.Tests.Strategies
{
    public class StrategyWithConstructorParameterReader : BaseStrategy
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

        public override bool Read(ConfigurationContext buildConfigurationContext)
        {
            return true;
        }
    }
}
