using System;
using System.Collections.Generic;
using System.Linq;
using FeatureSwitch.Strategies;

namespace FeatureSwitch
{
    public class FeatureContext
    {
        private readonly Dictionary<Type, Type> _additionalStrategies = new Dictionary<Type, Type>();
        private static bool _initialized;
        private static FeatureContext _instance = new FeatureContext();
        private static readonly object _syncRoot = new object();

        public FeatureContext()
        {
            Container = new FeatureSetContainer();
            AutoDiscoverFeatures = true;
        }

        public FeatureSetContainer Container { get; private set; }
        public bool AutoDiscoverFeatures { get; set; }

        internal IDictionary<Type, Type> AdditionalStrategies
        {
            get
            {
                return _additionalStrategies;
            }
        }

        public void AddConfigurationError(BaseFeature feature, string error)
        {
            Container.ConfigurationErrors.Add(feature.GetType().FullName, error);
        }

        public void AddFeature<T>() where T : BaseFeature
        {
            AddFeature(typeof(T));
        }

        public void AddFeature(Type featureType)
        {
            Container.AddFeature(featureType);
        }

        public static void Disable<T>() where T : BaseFeature
        {
            Disable(typeof(T).FullName);
        }

        public static void Disable(string featureName)
        {
            TestInstance();
            _instance.Container.ChangeEnabledState(featureName, false);
        }

        public static void Enable<T>() where T : BaseFeature
        {
            Enable(typeof(T).FullName);
        }

        public static void Enable(string featureName)
        {
            TestInstance();
            _instance.Container.ChangeEnabledState(featureName, true);
        }

        public StrategyConfigurationExpression<TStrategy> ForStrategy<TStrategy>() where TStrategy : FeatureStrategyAttribute
        {
            return new StrategyConfigurationExpression<TStrategy>(this);
        }

        public static BaseFeature GetFeature<T>(bool throwNotFound = true) where T : BaseFeature
        {
            return GetFeature(typeof(T), throwNotFound);
        }

        public static BaseFeature GetFeature(Type feature, bool throwNotFound = true)
        {
            return _instance.Container.GetFeature(feature, throwNotFound).Item1;
        }

        public static IList<BaseFeature> GetFeatures()
        {
            return _instance.Container.Features.Values.Select(t => t.Item1).ToList();
        }

        public static bool IsEnabled(BaseFeature feature)
        {
            return IsEnabled(feature.GetType());
        }

        public static bool IsEnabled(Type feature)
        {
            TestInstance();
            return _instance.Container.IsEnabled(feature);
        }

        public static bool IsEnabled<T>() where T : BaseFeature
        {
            return IsEnabled(typeof(T));
        }

        public static IEnumerable<IStrategy> GetEnabledStrategiesForFeature(Type feature)
        {
            TestInstance();
            return _instance.Container.GetEnabledStateStrategiesForFeature(feature);
        }

        public static IEnumerable<IStrategy> GetEnabledStrategiesForFeature<T>() where T : BaseFeature
        {
            return GetEnabledStrategiesForFeature(typeof(T));
        }

        internal static bool IsStrategyEnabled<T>() where T : IStrategy
        {
            TestInstance();
            return _instance.Container.IsStrategyEnabled(typeof(T));
        }

        internal static void SetInstance(FeatureContext context)
        {
            lock (_syncRoot)
            {
                if (context == null)
                {
                    _initialized = false;
                }
                else
                {
                    _instance = context;
                    _initialized = true;
                }
            }
        }

        private static void TestInstance()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Feature context is not initialized. Create new instance of FeatureSetBuilder and call Build() method.");
            }
        }
    }
}
