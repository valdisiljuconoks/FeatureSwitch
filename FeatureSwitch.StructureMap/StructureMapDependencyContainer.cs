using System;
using StructureMap;

namespace FeatureSwitch.StructureMap
{
    public class StructureMapDependencyContainer : IDependencyContainer
    {
        private readonly IContainer _container;

        public StructureMapDependencyContainer(IContainer container)
        {
            _container = container;
        }

        public StructureMapDependencyContainer()
        {
            _container = new Container(_ => _.AddRegistry<DefaultDependencyRegistry>());
        }

        public void Configure(Action<ConfigurationExpression> dependencyConfiguration)
        {
            dependencyConfiguration.WithNotNull(expr => _container.Configure(expr));
        }

        public void RegisterType(Type requestedType, Type implementation)
        {
            _container.Configure(c => c.For(requestedType).Use(implementation));
        }

        public object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }
    }
}
