namespace FeatureToggle.Strategies
{
    public abstract class BaseStrategy : IAppSettingsReader
    {
        public virtual void Initialize()
        {
        }

        public abstract bool Read(ConfigurationContext buildConfigurationContext);
    }
}
