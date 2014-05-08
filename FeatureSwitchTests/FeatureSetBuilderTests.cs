﻿using System;
using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;
using FeatureSwitch.Tests.Features;
using FeatureSwitch.Tests.Strategies;
using Xunit;

namespace FeatureSwitch.Tests
{
    public class FeatureSetBuilderTests
    {
        [Fact]
        public void BuilderTest_AddMoreFeature_Success()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MySampleFeature>();
                                              ctx.AddFeature<MySampleFeatureWithoutStrategy>();
                                              ctx.AddFeature<MyFancyStrategySampleFeature>();

                                              ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                                              ctx.ForStrategy<UnitTestsAlwaysTrueStrategy>().Use<AlwaysTrueStrategyImpl>();
                                          });

            Assert.True(container.IsEnabled<MySampleFeature>(), "MySampleFeature is not enabled");
            Assert.False(container.IsEnabled<MySampleFeatureWithoutStrategy>(), "MySampleFeatureWithoutStrategy is not enabled");
            Assert.True(container.IsEnabled<MyFancyStrategySampleFeature>(), "MyFancyStrategySampleFeature is not enabled");
        }

        [Fact]
        public void BuilderTest_AddSameFeatureMultipleTimes_NoFailure()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MySampleFeature>();
                                              ctx.AddFeature<MySampleFeature>();

                                              ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                                          });

            var isEnabled = container.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled);
        }

        [Fact]
        public void BuilderTest_CallContextWithoutSetup_ThrowsException()
        {
            // reset context - this is not a good style, yes I know
            // but test is for ensuring static type behavior
            FeatureContext.SetInstance(null);
            Assert.Throws<InvalidOperationException>(() => FeatureContext.IsEnabled<MySampleFeature>());
        }

        [Fact]
        public void BuilderTest_OneFeatureOneStrategy_Success()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MySampleFeature>();
                                              ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                                          });

            var isEnabled = container.IsEnabled<MySampleFeature>();

            Assert.True(isEnabled, "MySampleFeature is not enabled");
        }

        [Fact]
        public void BuilderTest_RequestForNonRegisteredFeature_FeatureDisabled()
        {
            var builder = new FeatureSetBuilder();
            var container = builder.Build(ctx =>
                                          {
                                              ctx.AddFeature<MySampleFeature>();
                                              ctx.ForStrategy<AppSettings>().Use<AlwaysTrueStrategyImpl>();
                                          });

            var isEnabled = container.IsEnabled<MySampleFeatureWithoutStrategy>();

            Assert.False(isEnabled);
        }
    }
}
