namespace FeatureSwitch
{
    public class ConfigurationContext
    {
        public ConfigurationContext(string key)
        {
            Key = key;
        }

        public string Key { get; private set; }
    }
}
