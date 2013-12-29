using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureToggle
{
    public class FeatureContext
    {
        private readonly Dictionary<Type, Type> additionalStrategies = new Dictionary<Type, Type>();
        private static bool initialized;
        private static FeatureContext instance = new FeatureContext();

        public FeatureContext()
        {
            Container = new FeatureSetContainer();
        }

        public FeatureSetContainer Container { get; private set; }

        internal IDictionary<Type, Type> AdditionalStrategies
        {
            get
            {
                return this.additionalStrategies;
            }
        }

        public void AddConfigurationError(string error)
        {
            Container.ConfigurationErrors.Add(error);
        }

        public void AddFeature<T>() where T : IFeature
        {
            AddFeature(typeof(T));
        }

        public void AddFeature(Type featureType)
        {
            Container.AddFeature(featureType);
        }

        public static void Disable<T>() where T : IFeature
        {
            Disable(typeof(T).FullName);
        }

        public static void Disable(string featureName)
        {
            TestInstance();
            instance.Container.ChangeEnabledState(featureName, false);
        }

        public static void Enable<T>() where T : IFeature
        {
            Enable(typeof(T).FullName);
        }

        public static void Enable(string featureName)
        {
            TestInstance();
            instance.Container.ChangeEnabledState(featureName, true);
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
            return instance.Container.GetFeature(feature, throwNotFound);
        }

        public static IList<BaseFeature> GetFeatures()
        {
            return instance.Container.Features.Values.Select(t => t.Item1).ToList();
        }

        public static bool IsEnabled(Type feature)
        {
            TestInstance();
            return instance.Container.IsEnabled(feature);
        }

        public static bool IsEnabled<T>() where T : IFeature
        {
            return IsEnabled(typeof(T));
        }

        internal static void SetInstance(FeatureContext context)
        {
            lock (typeof(FeatureContext))
            {
                if (context == null)
                {
                    initialized = false;
                }
                else
                {
                    instance = context;
                    initialized = true;
                }
            }
        }

        private static void TestInstance()
        {
            if (!initialized)
            {
                throw new InvalidOperationException("Feature context is not initialized. Create new instance of FeatureSetBuilder and call Build() method.");
            }
        }
    }
}
