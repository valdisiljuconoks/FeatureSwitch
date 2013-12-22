namespace FeatureToggle.Strategies
{
    public class AlwaysTrueStrategyReader : BaseStrategy
    {
        public override bool Read(ConfigurationContext buildConfigurationContext)
        {
            return true;
        }
    }
}
