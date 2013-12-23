namespace FeatureToggle.Strategies
{
    public class AlwaysFalseStrategyReader : BaseStrategy
    {
        public override bool Read(ConfigurationContext buildConfigurationContext)
        {
            return false;
        }
    }
}
