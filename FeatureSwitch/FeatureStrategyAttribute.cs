using System;

namespace FeatureSwitch
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class FeatureStrategyAttribute : Attribute
    {
        protected FeatureStrategyAttribute()
        {
            this.Order = 0;
        }

        public int Order { get; set; }

        public string Key { get; set; }

        public ConfigurationContext BuildConfigurationContext()
        {
            return new ConfigurationContext(this.Key);
        }
    }
}
