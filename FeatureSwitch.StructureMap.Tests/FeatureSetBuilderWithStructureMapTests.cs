using Xunit;

namespace FeatureSwitch.StructureMap.Tests
{
    using FeatureSwitch.Tests;
    using FeatureSwitch.Tests.Features;
    using FeatureSwitch.Tests.Strategies;

    public class FeatureSetBuilderWithStructureMapTests
    {
        [Fact]
        public void BuilderTest_CustomStrategyWithConstructorParameters_ThrowsExceptionWithSimpleContainer()
        {
            var dependencyContainer = new StructureMapDependencyContainer();
            dependencyContainer.Configure(expression => expression.For<ISampleInjectedInterface>().Use<SampleInjectedInterface>());

            var builder = new FeatureSetBuilder(dependencyContainer);
            var container = builder.Build(ctx =>
            {
                ctx.AddFeature<MySampleFeatureWithConstructorParameterStrategy>();
                ctx.ForStrategy<StrategyWithConstructorParameter>().Use<StrategyWithConstructorParameterReader>();
            });

            Assert.True(container.IsEnabled<MySampleFeatureWithConstructorParameterStrategy>());
            Assert.NotNull(((StrategyWithConstructorParameterReader)builder.GetStrategyImplementation<StrategyWithConstructorParameter>()).Dependency);
        }
    }
}