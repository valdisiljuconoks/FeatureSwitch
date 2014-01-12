using System;

namespace FeatureSwitch.Strategies.Implementations
{
    public abstract class BaseStrategyReaderImpl : IStrategyStorageReader
    {
        protected internal ConfigurationContext Context { get; set; }

        public virtual void Initialize(ConfigurationContext configurationContext)
        {
            this.Context = configurationContext;
        }

        public abstract bool Read();

        protected bool ConvertToBoolean(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
