using System;
using StructureMap;

namespace FeatureSwitch.StructureMap
{
    public class StructureMapDependencyContainer : IDependencyContainer
    {
        private readonly IContainer container;

        public StructureMapDependencyContainer(IContainer container)
        {
            this.container = container;
        }

        public StructureMapDependencyContainer()
        {
            ObjectFactory.Initialize(init => init.AddRegistry<DefaultDependencyRegistry>());
            this.container = ObjectFactory.Container;
        }

        public void Configure(Action<ConfigurationExpression> dependencyConfiguration)
        {
            dependencyConfiguration.WithNotNull(expr => this.container.Configure(expr));
        }

        public void RegisterType(Type requestedType, Type implementation)
        {
            this.container.Configure(c => c.For(requestedType).Use(implementation));
        }

        public object Resolve(Type type)
        {
            return this.container.GetInstance(type);
        }
    }
}
