using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FeatureToggle.Strategies;
using FeatureToggle.Strategies.Implementations;
using StructureMap;

namespace FeatureToggle
{
    public class FeatureSetBuilder
    {
        private IContainer container;
        private readonly Dictionary<Type, Type> defaultImplementations = new Dictionary<Type, Type>
        {
            { typeof(AppSettingsStrategy), typeof(AppSettingsStrategyImpl) },
            { typeof(AlwaysTrueStrategy), typeof(AlwaysTrueStrategyImpl) },
            { typeof(AlwaysFalseStrategy), typeof(AlwaysFalseStrategyImpl) },
            { typeof(HttpSession), typeof(HttpSessionStrategyImpl) },
        };

        public FeatureSetBuilder(IContainer container = null)
        {
            this.container = container;
        }

        public Dictionary<Type, Type> Readers
        {
            get
            {
                return this.defaultImplementations;
            }
        }

        public FeatureSetContainer Build(Action<FeatureContext> action = null, Action<ConfigurationExpression> dependencyConfiguration = null)
        {
            lock (typeof(FeatureSetBuilder))
            {
                var context = SetupFeatureContext(action);
                SetupDependencyContainer(dependencyConfiguration, context);
                BuildFeatureSet(context);
                DetectCollisions(context);

                FeatureContext.SetInstance(context);

                return context.Container;
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
            foreach (var readerKeyValuePair in context.AdditionalStrategies)
            {
                var strategyType = readerKeyValuePair.Key;
                var strategyReaderType = readerKeyValuePair.Value;

                if (this.defaultImplementations.Keys.Contains(strategyType))
                {
                    // swap already registered strategy
                    this.defaultImplementations[strategyType] = strategyReaderType;
                }
                else
                {
                    this.defaultImplementations.Add(strategyType, strategyReaderType);
                }

                // TODO: review this
                if (!strategyReaderType.IsInterface)
                {
                    // we can create implementation only for concrete types
                    // if registered reader is interface - most probably it's registered in via IoC registry already
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

        internal IStrategy GetStrategyImplementation(Type strategyType)
        {
            Type reader;
            return this.defaultImplementations.TryGetValue(strategyType, out reader) ? (IStrategy)this.container.GetInstance(reader) : new EmptyStrategy();
        }

        internal IStrategy GetStrategyImplementation<T>()
        {
            return GetStrategyImplementation(typeof(T));
        }

        private void BuildFeatureSet(FeatureContext context)
        {
            // configure and setup features
            foreach (var keyValuePair in context.Container.Features)
            {
                // build list of strategies and corresponding implementations for this feature
                var strategies = keyValuePair.Value.Item1.GetType().GetCustomAttributes<FeatureStrategyAttribute>().OrderBy(a => a.Order);

                if (!strategies.Any())
                {
                    continue;
                }

                // test if there are any strategy with equal order
                if (strategies.GroupBy(a => a.Order).Any(k => k.Count() > 1))
                {
                    context.AddConfigurationError(string.Format("Feature {0} has strategies with the same order.", keyValuePair.Key));
                }

                var strategyImplementations = strategies.Select(s => Tuple.Create(s, GetStrategyImplementation(s.GetType()))).ToList();

                keyValuePair.Value.Item2.Clear();
                strategyImplementations.ForEach(i => keyValuePair.Value.Item2.Add(i.Item2));

                strategyImplementations.ForEach(i => i.Item2.Initialize(i.Item1.BuildConfigurationContext()));
                var states = strategyImplementations.Select(k =>
                                                            {
                                                                // test if strategy implementation is readable
                                                                var reader = k.Item2 as IStrategyStorageReader;
                                                                return reader != null && reader.Read();
                                                            });

                // feature is enabled if any of strategies is telling truth
                keyValuePair.Value.Item1.ChangeEnabledState(states.Any(b => b));

                // do we have any writer in da house?
                keyValuePair.Value.Item1.ChangeModifiableState(strategyImplementations.Any(s => s.Item2 is IStrategyStorageWriter));
            }
        }

        private void DetectCollisions(FeatureContext context)
        {
            // TODO: implement this
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
