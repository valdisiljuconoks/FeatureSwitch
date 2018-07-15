namespace FeatureSwitch
{
    public class ConfigurationContext
    {
        public ConfigurationContext(FeatureStrategyAttribute featureStrategyAttribute)
        {
            FeatureStrategyAttribute = featureStrategyAttribute;
        }

        public FeatureStrategyAttribute FeatureStrategyAttribute { get; }

        public string Key => FeatureStrategyAttribute.Key;
    }
}
