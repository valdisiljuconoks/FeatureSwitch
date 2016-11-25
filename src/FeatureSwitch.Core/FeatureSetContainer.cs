using System;
using System.Collections.Generic;
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

        public IDictionary<string, Tuple<BaseFeature, IList<IStrategy>>> Features => _features;

        public IDictionary<string, string> ConfigurationErrors { get; }

        public void AddFeature<T>() where T : BaseFeature
        {
            AddFeature(typeof(T));
        }

        public void AddFeature(Type featureType)
        {
            var key = featureType.FullName;

            // add only if does not exist
            if(_features.ContainsKey(key))
            {
                return;
            }

            var featureInstance = (BaseFeature) Activator.CreateInstance(featureType);
            featureInstance.Name = featureType.Name;

            _features.Add(key, Tuple.Create<BaseFeature, IList<IStrategy>>(featureInstance, new List<IStrategy>()));
        }

        public BaseFeature GetFeature<T>(bool throwNotFound = true) where T : BaseFeature
        {
            var featureWithStrategy = GetFeature(typeof(T), throwNotFound);
            return featureWithStrategy?.Item1;
        }

        public Tuple<BaseFeature, IList<IStrategy>> GetFeature(Type feature, bool throwNotFound = true)
        {
            var item = GetFeatureWithStrategies(feature.FullName);
            if(item != null)
                return item;

            if(throwNotFound)
                throw new KeyNotFoundException("Feature of type" + feature + " not found");

            return null;
        }

        public bool IsEnabled(Type feature)
        {
            return GetEnabledStateStrategiesForFeature(feature).Any();
        }

        public bool IsEnabled<T>() where T : BaseFeature
        {
            return IsEnabled(typeof(T));
        }

        public IEnumerable<IStrategy> GetEnabledStateStrategiesForFeature(Type feature)
        {
            if(ConfigurationErrors.Keys.Contains(feature.FullName, StringComparer.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(ConfigurationErrors[feature.FullName]);
            }

            var f = GetFeature(feature, false);

            if(f == null)
            {
                return Enumerable.Empty<IStrategy>();
            }

            var enabledStrategies = f.Item2.Where(s =>
                                                  {
                                                      // test if strategy implementation is readable
                                                      var reader = s as IStrategyStorageReader;
                                                      return reader != null && reader.Read();
                                                  }).Select(s => s).ToList();

            // feature is enabled if any of strategies is telling the truth
            return enabledStrategies;
        }

        public IEnumerable<IStrategy> GetEnabledStateStrategiesForFeature<T>() where T : BaseFeature
        {
            return GetEnabledStateStrategiesForFeature(typeof(T));
        }

        public void ValidateConfiguration()
        {
            if(ConfigurationErrors.Any())
            {
                throw new InvalidOperationException(string.Join("; ", ConfigurationErrors));
            }
        }

        internal void ChangeEnabledState(string featureName, bool state)
        {
            var item = GetFeatureWithStrategies(featureName);

            if(item == null)
            {
                throw new KeyNotFoundException("Feature of type " + featureName + " not found");
            }

            // find 1st writer strategy
            var writer = item.Item2.FirstOrDefault(s => s is IStrategyStorageWriter);
            if(writer == null)
            {
                throw new InvalidOperationException("Feature of type " + featureName + " is not modifiable");
            }

            try
            {
                ((IStrategyStorageWriter) writer).Write(state);
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

        internal bool IsEnabled<T>(Type strategy) where T : BaseFeature
        {
            var f = GetFeature(typeof(T), false);
            var matchStrategy = f.Item2.FirstOrDefault(s => s.GetType() == strategy);

            return matchStrategy != null && ((IStrategyStorageReader) matchStrategy).Read();
        }

        internal bool IsStrategyEnabled(Type strategyType)
        {
            var allStrategies = Features.SelectMany(f => f.Value.Item2).Distinct();
            return allStrategies.Any(strategyType.IsInstanceOfType);
        }

        internal Tuple<BaseFeature, IList<IStrategy>> GetFeatureWithStrategies(string featureName)
        {
            var featureEntry = _features.FirstOrDefault(f => f.Key != null && f.Key == featureName);

            return new Tuple<BaseFeature, IList<IStrategy>>(featureEntry.Value.Item1,
                                                            featureEntry.Value.Item2.Select(s => FeatureContext.DependencyContainer.Resolve(s.GetType()) as IStrategy).ToList());

            //return featureEntry.Key != null ? featureEntry.Value : null;
        }
    }
}
