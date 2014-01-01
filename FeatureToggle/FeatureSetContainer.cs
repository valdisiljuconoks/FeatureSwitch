using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FeatureToggle.Strategies;

namespace FeatureToggle
{
    public class FeatureSetContainer
    {
        private readonly Dictionary<string, Tuple<BaseFeature, IList<IStrategy>>> features = new Dictionary<string, Tuple<BaseFeature, IList<IStrategy>>>();

        public FeatureSetContainer()
        {
            ConfigurationErrors = new List<string>();
        }

        public IDictionary<string, Tuple<BaseFeature, IList<IStrategy>>> Features
        {
            get
            {
                return this.features;
            }
        }

        public List<string> ConfigurationErrors { get; private set; }

        public void AddFeature<T>() where T : IFeature
        {
            AddFeature(typeof(T));
        }

        public void AddFeature(Type featureType)
        {
            var key = featureType.FullName;

            // add only if does not exist
            if (this.features.ContainsKey(key))
            {
                return;
            }

            var featureInstance = (BaseFeature)Activator.CreateInstance(featureType);
            featureInstance.Name = featureType.Name;

            this.features.Add(key, Tuple.Create<BaseFeature, IList<IStrategy>>(featureInstance, new List<IStrategy>()));
        }

        public IFeature GetFeature<T>(bool throwNotFound = true) where T : IFeature
        {
            return GetFeature(typeof(T), throwNotFound);
        }

        public IFeature GetFeature(Type feature, bool throwNotFound = true)
        {
            var item = GetFeatureWithStrategies(feature.FullName);
            if (item != null)
            {
                return item.Item1;
            }

            if (throwNotFound)
            {
                throw new KeyNotFoundException("Feature of type" + feature + " not found");
            }

            return null;
        }

        public bool IsEnabled(Type feature)
        {
            var f = GetFeature(feature, false);
            return f != null && f.IsEnabled;
        }

        public bool IsEnabled<T>() where T : IFeature
        {
            return IsEnabled(typeof(T));
        }

        public void ValidateConfiguration()
        {
            if (ConfigurationErrors.Any())
            {
                throw new ConfigurationErrorsException(string.Join("; ", ConfigurationErrors));
            }
        }

        internal void ChangeEnabledState(string featureName, bool state)
        {
            var item = GetFeatureWithStrategies(featureName);

            if (item == null)
            {
                throw new KeyNotFoundException("Feature of type" + featureName + " not found");
            }

            // find 1st writer strategy
            var writer = item.Item2.FirstOrDefault(s => s is IStrategyStorageWriter);
            if (writer == null)
            {
                throw new InvalidOperationException("Feature of type " + featureName + " is not modifiable");
            }

            try
            {
                ((IStrategyStorageWriter)writer).Write(state);
                item.Item1.ChangeEnabledState(state);
            }
            catch (Exception e)
            {
                // TODO: add extension point for logging
            }
        }

        internal void ChangeEnabledState<T>(bool state) where T : IFeature
        {
            ChangeEnabledState(typeof(T).FullName, state);
        }

        private Tuple<BaseFeature, IList<IStrategy>> GetFeatureWithStrategies(string featureName)
        {
            var featureEntry = this.features.FirstOrDefault(f => f.Key != null && f.Key == featureName);
            return featureEntry.Key != null ? featureEntry.Value : null;
        }
    }
}
