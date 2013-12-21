namespace FeatureToggle.Strategies
{
    public class AppSettingsStrategy : FeatureStrategyAttribute
    {
        public string Key { get; set; }

        public override ConfigurationContext BuildConfigurationContext()
        {
            return new ConfigurationContext
            {
                Key = Key
            };
        }
    }
}
