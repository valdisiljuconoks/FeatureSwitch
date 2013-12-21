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
        private static FeatureContext instance;

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
                this.features.Add(featureType, Tuple.Create<BaseFeature, IList<IStrategy>>((BaseFeature)Activator.CreateInstance(featureType), null));
            }
        }

        public static IFeature GetFeature<T>(bool throwNotFound = true) where T : IFeature
        {
            var featureEntry = instance.features.FirstOrDefault(f => f.Key != null && f.Key.IsAssignableFrom(typeof(T)));
            if (featureEntry.Key != null)
            {
                return featureEntry.Value.Item1;
            }

            if (throwNotFound)
            {
                throw new KeyNotFoundException("Feature of type" + typeof(T) + " not found");
            }

            return null;
        }

        public StrategyConfigurationExpression<TStrategy> ForStrategy<TStrategy>() where TStrategy : FeatureStrategyAttribute
        {
            return new StrategyConfigurationExpression<TStrategy>(this);
        }

        public static bool IsEnabled<T>() where T : IFeature
        {
            var f = GetFeature<T>(false);
            return f != null && f.IsEnabled;
        }

        internal static void SetInstance(FeatureContext context)
        {
            instance = context;
        }
    }
}
