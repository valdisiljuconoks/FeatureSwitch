using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FeatureSwitch.Strategies;

namespace FeatureSwitch
{
    public class FeatureSetContainer
    {
        private readonly Dictionary<string, Tuple<BaseFeature, IList<IStrategy>>> _features = new Dictionary<string, Tuple<BaseFeature, IList<IStrategy>>>();

        public FeatureSetContainer()
        {
            ConfigurationErrors = new Dictionary<string, string>();
        }

        public IDictionary<string, Tuple<BaseFeature, IList<IStrategy>>> Features
        {
            get
            {
                return _features;
            }
        }

        public IDictionary<string, string> ConfigurationErrors { get; private set; }

        public void AddFeature<T>() where T : BaseFeature
        {
            AddFeature(typeof(T));
        }

        public void AddFeature(Type featureType)
        {
            var key = featureType.FullName;

            // add only if does not exist
            if (_features.ContainsKey(key))
            {
                return;
            }

            var featureInstance = (BaseFeature)Activator.CreateInstance(featureType);
            featureInstance.Name = featureType.Name;

            _features.Add(key, Tuple.Create<BaseFeature, IList<IStrategy>>(featureInstance, new List<IStrategy>()));
        }

        public BaseFeature GetFeature<T>(bool throwNotFound = true) where T : BaseFeature
        {
            var featureWithStrategy = GetFeature(typeof (T), throwNotFound);
            return featureWithStrategy != null ? featureWithStrategy.Item1 : null;
        }

        public Tuple<BaseFeature, IList<IStrategy>> GetFeature(Type feature, bool throwNotFound = true)
        {
            var item = GetFeatureWithStrategies(feature.FullName);
            if (item != null)
            {
                return item;
            }

            if (throwNotFound)
            {
                throw new KeyNotFoundException("Feature of type" + feature + " not found");
            }

            return null;
        }

        public bool IsEnabled(Type feature)
        {
            if (ConfigurationErrors.Keys.Contains(feature.FullName, StringComparer.InvariantCultureIgnoreCase))
            {
                throw new ConfigurationErrorsException(ConfigurationErrors[feature.FullName]);
            }

            var f = GetFeature(feature, false);

            if (f == null)
            {
                return false;
            }

            var states = f.Item2.Select(s =>
            {
                // test if strategy implementation is readable
                var reader = s as IStrategyStorageReader;
                return reader != null && reader.Read();
            });

            // feature is enabled if any of strategies is telling truth
            return states.Any(b => b);
        }

        public bool IsEnabled<T>() where T : BaseFeature
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
            }
            catch (Exception)
            {
                // TODO: add extension point for logging
            }
        }

        internal void ChangeEnabledState<T>(bool state) where T : BaseFeature
        {
            ChangeEnabledState(typeof(T).FullName, state);
        }

        internal bool IsStrategyEnabled(Type strategyType)
        {
            var allStrategies = Features.SelectMany(f => f.Value.Item2).Distinct();
            return allStrategies.Any(strategyType.IsInstanceOfType);
        }

        private Tuple<BaseFeature, IList<IStrategy>> GetFeatureWithStrategies(string featureName)
        {
            var featureEntry = _features.FirstOrDefault(f => f.Key != null && f.Key == featureName);
            return featureEntry.Key != null ? featureEntry.Value : null;
        }
    }
}
