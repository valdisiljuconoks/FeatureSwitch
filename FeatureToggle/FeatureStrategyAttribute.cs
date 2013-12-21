using System;

namespace FeatureToggle
{
    public abstract class FeatureStrategyAttribute : Attribute
    {
        protected FeatureStrategyAttribute()
        {
            Order = 0;
        }

        public int Order { get; set; }

        public virtual ConfigurationContext BuildConfigurationContext()
        {
            return new ConfigurationContext();
        }
    }
}
