using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FeatureToggle.Strategies;

namespace FeatureToggle
{
    public class FeatureSetContainer
    {
        private readonly Dictionary<Type, Tuple<BaseFeature, IList<IStrategy>>> features = new Dictionary<Type, Tuple<BaseFeature, IList<IStrategy>>>();

        public FeatureSetContainer()
        {
            ConfigurationErrors = new List<string>();
        }

        public IReadOnlyDictionary<Type, Tuple<BaseFeature, IList<IStrategy>>> Features
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
            // add only if does not exist
            if (this.features.ContainsKey(featureType))
            {
                return;
            }

            var featureInstance = (BaseFeature)Activator.CreateInstance(featureType);
            featureInstance.Name = featureType.Name;

            this.features.Add(featureType, Tuple.Create<BaseFeature, IList<IStrategy>>(featureInstance, new List<IStrategy>()));
        }

        public IFeature GetFeature<T>(bool throwNotFound = true) where T : IFeature
        {
            return GetFeature(typeof(T), throwNotFound);
        }

        public IFeature GetFeature(Type feature, bool throwNotFound = true)
        {
            var item = GetFeatureWithStrategies(feature);
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

        internal void ChangeEnabledState<T>(bool state) where T : IFeature
        {
            var item = GetFeatureWithStrategies(typeof(T));

            if (item == null)
            {
                throw new KeyNotFoundException("Feature of type" + typeof(T) + " not found");
            }

            // find 1st writer strategy
            var writer = item.Item2.FirstOrDefault(s => s is IStrategyStorageWriter);
            if (writer == null)
            {
                throw new InvalidOperationException("Feature of type " + typeof(T) + " is not modifiable");
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

        private Tuple<BaseFeature, IList<IStrategy>> GetFeatureWithStrategies(Type feature)
        {
            var featureEntry = this.features.FirstOrDefault(f => f.Key != null && f.Key.IsAssignableFrom(feature));
            return featureEntry.Key != null ? featureEntry.Value : null;
        }
    }
}
