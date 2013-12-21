namespace FeatureToggle.Strategies
{
    public class AlwaysTrueStrategy : BaseStrategy
    {
        public override bool Read(ConfigurationContext buildConfigurationContext)
        {
            return true;
        }
    }
}