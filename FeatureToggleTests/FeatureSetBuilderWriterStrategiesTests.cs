using System;
using System.IO;
using FeatureToggle.Strategies;
using FeatureToggle.Tests.Features;
using StructureMap;
using Xunit;

namespace FeatureToggle.Tests
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
        public void BuilderTest_FeatureWithWritableStrategy_FeatureCanDisable()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                              ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyImpl>();
                          });

            var feature = FeatureContext.GetFeature<MyWritableFeatureSingleStrategy>();
            FeatureContext.Disable<MyWritableFeatureSingleStrategy>();

            Assert.False(feature.IsEnabled);
            // retrieve once again from the context
            var modifiedFeature = FeatureContext.GetFeature<MyWritableFeatureSingleStrategy>();
            Assert.False(modifiedFeature.IsEnabled);
        }

        [Fact]
        public void BuilderTest_FeatureWithWritableStrategy_FeatureCanEnable()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                              ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyImpl>();
                          });

            var feature = FeatureContext.GetFeature<MyWritableFeatureSingleStrategy>();

            Assert.False(feature.IsEnabled);
            Assert.True(feature.CanModify, "Feature is not modifiable");

            FeatureContext.Enable<MyWritableFeatureSingleStrategy>();

            // retrieve once again from the context
            var modifiedFeature = FeatureContext.GetFeature<MyWritableFeatureSingleStrategy>();
            Assert.True(modifiedFeature.IsEnabled);
        }

        [Fact]
        public void BuilderTest_MultipleFeaturesSameStrategy_NoCollisionsInState()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
                          {
                              ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                              ctx.AddFeature<MyWritableAnotherFeatureSingleStrategy>();
                              ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyImpl>();
                          });

            var feature1 = FeatureContext.GetFeature<MyWritableFeatureSingleStrategy>();
            var feature2 = FeatureContext.GetFeature<MyWritableAnotherFeatureSingleStrategy>();
            FeatureContext.Enable<MyWritableAnotherFeatureSingleStrategy>();

            Assert.False(feature1.IsEnabled);
            Assert.True(feature2.IsEnabled);

            FeatureContext.Enable<MyWritableFeatureSingleStrategy>();
            FeatureContext.Disable<MyWritableAnotherFeatureSingleStrategy>();

            Assert.True(FeatureContext.GetFeature<MyWritableFeatureSingleStrategy>().IsEnabled);
            Assert.False(FeatureContext.GetFeature<MyWritableAnotherFeatureSingleStrategy>().IsEnabled);
        }

        [Fact]
        public void BuilderTest_FeatureWithWritableStrategy_FailToWrite_FeatureDoesNotChangeState()
        {
            var builder = new FeatureSetBuilder(new Container());
            builder.Build(ctx =>
            {
                ctx.AddFeature<MyWritableFeatureSingleStrategy>();
                ctx.ForStrategy<WritableStrategy>().Use<WritableStrategyExceptionImpl>();
            });

            var feature1 = FeatureContext.GetFeature<MyWritableFeatureSingleStrategy>();

            Assert.False(feature1.IsEnabled);
            FeatureContext.Enable<MyWritableFeatureSingleStrategy>();
            Assert.False(feature1.IsEnabled);
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
