using System;
using System.IO;
using FeatureSwitch.Strategies;
using FeatureSwitch.Tests.Features;
using StructureMap;
using Xunit;

namespace FeatureSwitch.Tests
{
    public class FeatureSetBuilderWriterStrategiesTests
    {
        [Fact]
        public void BuilderTest_FeatureWithReadOnlyStrategy_FailedEnable()
        {
            var builder = new FeatureSetBuilder();
            builder.Build(ctx => ctx.AddFeature<MySampleFeature>());

            Assert.Throws<InvalidOperationException>(() => FeatureContext.Enable<MySampleFeature>());
        }

        [Fact]
        public void BuilderTest_FeatureWithWritableStrategy_FailToWrite_FeatureDoesNotChangeState()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                                              ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyExceptionImpl>();
                                          });

            Assert.False(container.IsEnabled<MyWritableFeatureSingleStrategy>());
            FeatureContext.Enable<MyWritableFeatureSingleStrategy>();

            Assert.False(container.IsEnabled<MyWritableFeatureSingleStrategy>());
        }

        [Fact]
        public void BuilderTest_FeatureWithWritableStrategy_FeatureCanBeEnabled()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                                              ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyImpl>();
                                          });

            Assert.False(container.IsEnabled<MyWritableFeatureSingleStrategy>());
            Assert.True(container.GetFeature<MyWritableFeatureSingleStrategy>().CanModify, "Feature is not modifiable");

            FeatureContext.Enable<MyWritableFeatureSingleStrategy>();

            Assert.True(container.IsEnabled<MyWritableFeatureSingleStrategy>());
        }

        [Fact]
        public void BuilderTest_FeatureWithWritableStrategy_FeatureCanDisable()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                                              ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyImpl>();
                                          });

            FeatureContext.Disable<MyWritableFeatureSingleStrategy>();

            Assert.False(container.IsEnabled<MyWritableFeatureSingleStrategy>());
        }

        [Fact]
        public void BuilderTest_MultipleFeaturesSameStrategy_NoCollisionsInState()
        {
            var builder = new FeatureSetBuilder(new Container());
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                                              ctx.AddFeature<MyWritableAnotherFeatureSingleStrategy>();
                                              ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyImpl>();
                                          });

            FeatureContext.Enable<MyWritableAnotherFeatureSingleStrategy>();

            Assert.False(FeatureContext.IsEnabled<MyWritableFeatureSingleStrategy>());
            Assert.True(FeatureContext.IsEnabled<MyWritableAnotherFeatureSingleStrategy>());

            FeatureContext.Enable<MyWritableFeatureSingleStrategy>();
            FeatureContext.Disable<MyWritableAnotherFeatureSingleStrategy>();

            Assert.True(FeatureContext.IsEnabled<MyWritableFeatureSingleStrategy>());
            Assert.False(FeatureContext.IsEnabled<MyWritableAnotherFeatureSingleStrategy>());
        }
    }

    [WritableStrategy(Key = "SampleKey")]
    public class MyWritableFeatureSingleStrategy : BaseFeature
    {
    }

    [WritableStrategy(Key = "SampleKey2")]
    public class MyWritableAnotherFeatureSingleStrategy : BaseFeature
    {
    }

    public class WritableStrategy : FeatureStrategyAttribute
    {
    }

    public class WritableStrategyImpl : BaseStrategyImpl
    {
        private bool isEnabled;

        public override bool Read()
        {
            return this.isEnabled;
        }

        public override void Write(bool state)
        {
            this.isEnabled = state;
        }
    }

    public class WritableStrategyExceptionImpl : BaseStrategyImpl
    {
        public override bool Read()
        {
            return false;
        }

        public override void Write(bool state)
        {
            throw new FileNotFoundException();
        }
    }
}
