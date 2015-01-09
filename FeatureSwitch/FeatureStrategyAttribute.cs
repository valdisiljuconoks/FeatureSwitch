using System;
using FeatureSwitch.Strategies;

namespace FeatureSwitch
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class FeatureStrategyAttribute : Attribute
    {
        protected FeatureStrategyAttribute()
        {
            Order = 0;
        }

        public int Order { get; set; }

        public string Key { get; set; }

        public virtual Type DefaultImplementation
        {
            get
            {
                return null;
            }
        }

        public ConfigurationContext BuildConfigurationContext(BaseFeature feature, IStrategy strategy)
        {
            if (string.IsNullOrWhiteSpace(Key))
            {
                throw new ArgumentException("Missing 'Key' parameter for '" + strategy.GetType().Name + "' strategy for '" + feature.Name + "' feature");
            }

            return new ConfigurationContext(Key);
        }
    }
}
