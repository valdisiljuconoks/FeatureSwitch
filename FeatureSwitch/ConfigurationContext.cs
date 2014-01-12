namespace FeatureSwitch
{
    public class ConfigurationContext
    {
        public ConfigurationContext(string key)
        {
            this.Key = key;
        }

        public string Key { get; private set; }
    }
}
