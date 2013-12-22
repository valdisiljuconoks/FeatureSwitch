using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FeatureToggle.Strategies;
using StructureMap;

namespace FeatureToggle
{
    public class FeatureSetBuilder
    {
        private IContainer container;
        private readonly Dictionary<Type, Type> defaultReaders = new Dictionary<Type, Type>
        {
            { typeof(AppSettingsStrategy), typeof(IAppSettingsReader) },
            { typeof(AlwaysTrueStrategy), typeof(AlwaysTrueStrategyReader) }
        };

        public FeatureSetBuilder(IContainer container = null)
        {
            this.container = container;
        }

        public Dictionary<Type, Type> Readers
        {
            get
            {
                return this.defaultReaders;
            }
        }

        public void Build(Action<FeatureContext> action = null, Action<ConfigurationExpression> dependencyConfiguration = null)
        {
            lock (typeof(FeatureSetBuilder))
            {
                var context = SetupFeatureContext(action);
                SetupDependencyContainer(dependencyConfiguration, context);
                BuildFeatureSet(context);

                FeatureContext.SetInstance(context);
            }
        }

        protected void SetupDependencyContainer(Action<ConfigurationExpression> dependencyConfiguration, FeatureContext context)
        {
            // setup dependency container
            if (this.container == null)
            {
                ObjectFactory.Initialize(init => init.AddRegistry<DefaultDependencyRegistry>());
                this.container = ObjectFactory.Container;
            }

            dependencyConfiguration.WithNotNull(expr => this.container.Configure(expr));

            // register additional or swapped strategies
            foreach (var readerKeyValuePair in context.Readers)
            {
                var strategyType = readerKeyValuePair.Key;
                var strategyReaderType = readerKeyValuePair.Value;

                if (this.defaultReaders.Keys.Contains(strategyType))
                {
                    // swap already registered strategy
                    this.defaultReaders[strategyType] = strategyReaderType;
                }
                else
                {
                    this.defaultReaders.Add(strategyType, strategyReaderType);
                }

                if (!strategyReaderType.IsInterface)
                {
                    // we can create implementation only for concrete types
                    // if registered reader is interface - most probably it's registered in via IoC registry already

                    // TODO: Add support for .ctor injections
                    this.container.Inject(strategyReaderType, this.container.GetInstance(strategyReaderType));
                }
            }
        }

        protected FeatureContext SetupFeatureContext(Action<FeatureContext> action)
        {
            var context = new FeatureContext();
            if (action != null)
            {
                // if configuration expression is present - call that one
                action(context);
            }
            else
            {
                // otherwise we are going to scan for all features exposed and add to the context
                DiscoverFeatures(context);
            }

            return context;
        }

        internal IStrategyStorageReader GetStrategyReader(Type strategyType)
        {
            Type reader;
            return !this.defaultReaders.TryGetValue(strategyType, out reader) ? null : (IStrategyStorageReader)this.container.TryGetInstance(reader);
        }

        internal IStrategyStorageReader GetStrategyReader<T>()
        {
            return GetStrategyReader(typeof(T));
        }

        private void BuildFeatureSet(FeatureContext context)
        {
            // configure and setup discovered features
            foreach (var feature in context.Features)
            {
                var strategies = feature.Value.Item1.GetType().GetCustomAttributes<FeatureStrategyAttribute>().OrderBy(a => a.Order);
                var states =
                    (from strategy in strategies
                     let reader = GetStrategyReader(strategy.GetType())
                     where reader != null
                     select reader.Read(strategy.BuildConfigurationContext()));

                // feature is enabled if any of strategies is telling truth
                feature.Value.Item1.ChangeState(states.Any(b => b));
            }
        }

        private void DiscoverFeatures(FeatureContext context)
        {
            context.CheckNull("context");
            foreach (var feature in TypeAttributeHelper.GetTypesChildOf<BaseFeature>())
            {
                context.AddFeature(feature);
            }
        }
    }
}
