namespace FeatureToggle.Strategies
{
    public abstract class BaseStrategyReaderImpl : IStrategyStorageReader
    {
        protected internal ConfigurationContext Context { get; set; }

        public virtual void Initialize(ConfigurationContext configurationContext)
        {
            Context = configurationContext;
        }

        public abstract bool Read();
    }
}
