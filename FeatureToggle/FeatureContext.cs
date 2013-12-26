using System;
using System.Collections.Generic;
using System.Linq;
using FeatureToggle.Strategies;

namespace FeatureToggle
{
    public class FeatureContext
    {
        private readonly Dictionary<Type, Type> additionalReaders = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Tuple<BaseFeature, IList<IStrategy>>> features = new Dictionary<Type, Tuple<BaseFeature, IList<IStrategy>>>();
        private static FeatureContext instance = new FeatureContext();
        private static bool initialized;

        public IReadOnlyDictionary<Type, Tuple<BaseFeature, IList<IStrategy>>> Features
        {
            get
            {
                return this.features;
            }
        }

        internal IDictionary<Type, Type> Readers
        {
            get
            {
                return this.additionalReaders;
            }
        }

        public void AddFeature<T>() where T : IFeature
        {
            AddFeature(typeof(T));
        }

        public void AddFeature(Type featureType)
        {
            // add only if does not exist
            if (!this.features.ContainsKey(featureType))
            {
                this.features.Add(featureType,
                                  Tuple.Create<BaseFeature, IList<IStrategy>>((BaseFeature)Activator.CreateInstance(featureType),
                                                                              new List<IStrategy>()));
            }
        }

        public static void Disable<T>() where T : IFeature
        {
            ChangeEnabledState<T>(false);
        }

        public static void Enable<T>() where T : IFeature
        {
            ChangeEnabledState<T>(true);
        }

        public StrategyConfigurationExpression<TStrategy> ForStrategy<TStrategy>() where TStrategy : FeatureStrategyAttribute
        {
            return new StrategyConfigurationExpression<TStrategy>(this);
        }

        public static IFeature GetFeature<T>(bool throwNotFound = true) where T : IFeature
        {
            return GetFeature(typeof(T), throwNotFound);
        }

        public static IFeature GetFeature(Type feature, bool throwNotFound = true)
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

        public static bool IsEnabled(Type feature)
        {
            var f = GetFeature(feature, false);
            return f != null && f.IsEnabled;
        }

        public static bool IsEnabled<T>() where T : IFeature
        {
            return IsEnabled(typeof(T));
        }

        internal static void SetInstance(FeatureContext context)
        {
            lock (typeof(FeatureContext))
            {
                instance = context;
                initialized = true;
            }
        }

        private static void ChangeEnabledState<T>(bool state) where T : IFeature
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

        private static Tuple<BaseFeature, IList<IStrategy>> GetFeatureWithStrategies(Type feature)
        {
            if (!initialized)
            {
                throw new InvalidOperationException("Feature context is not initialized. Create new instance of FeatureSetBuilder and call Build() method.");
            }
            var featureEntry = instance.features.FirstOrDefault(f => f.Key != null && f.Key.IsAssignableFrom(feature));
            return featureEntry.Key != null ? featureEntry.Value : null;
        }
    }
}
