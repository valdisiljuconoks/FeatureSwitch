namespace FeatureSwitch
{
    public class ConfigurationContext
    {
        public ConfigurationContext(FeatureStrategyAttribute featureStrategyAttribute)
        {
            this.FeatureStrategyAttribute = featureStrategyAttribute;
        }

        public FeatureStrategyAttribute FeatureStrategyAttribute { get; private set; }
        public string Key { get { return FeatureStrategyAttribute.Key; } }
    }
}
