FeatureToggle
=============

## Feature Toggling Framework for Various Types of Applications
Have you ever wrote the code like following to verify that either you have to disable or enable some functionality base on set of conditions (usually different sources):

```csharp
  if(ConfigurationManager.AppSettings["MyKey"] == "true")
      return ...;
```

or something like this:

```csharp
  if(HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["MyKey"] == "true")
      return ...;
```

FeatureSwitch library should reduce amount of time and code needed to implement feature toggle in unified way.
FeatureSwitch library is easily adoptable and extendable.

## Overview
FeatureSwitch library is based on two basic aspects [features](https://github.com/valdisiljuconoks/FeatureSwitch/wiki#features) and [strategies](https://github.com/valdisiljuconoks/FeatureSwitch/wiki#strategies). In combination they provide enough input data for feature set builder to construct feature context to be used later to check whether particular feature is enabled or disabled.
Currently there are following additional integrations:
* [Mvc Control Panel](https://github.com/valdisiljuconoks/FeatureSwitch/wiki/Asp.Net-MVC-Integration) - FeatureSwitch UI Control Panel
* [Web Optimization](https://github.com/valdisiljuconoks/FeatureSwitch/wiki/Web-Optimization-Helpers) - Helpers for styles and scripts conditional bundling and minification
* [EPiServer integration](https://github.com/valdisiljuconoks/FeatureSwitch/wiki/EPiServer-Integration) - Module to integrate into EPiServer CMS platform for easier access to UI Control Panel

## Features
Feature is main aspect of FeatureSwitch library it's your logical feature representation - either it's enabled or disabled. You will need to define your features to work with FeatureSwitch library.
To define your feature you have to create class that inherits from `FeatureSwitch.BaseFeature` class:

```csharp
  public class MySampleFeature : FeatureSwitch.BaseFeature
  {
  }
```

Keep in mind that you will need to add at least one [strategy](https://github.com/valdisiljuconoks/FeatureSwitch/wiki#strategies) for the feature in order to enable it.
By default every feature is **disabled**.


## Setup (FeatureSet builder)
To get started with FeatureSwitch you need to kick-off [FeatureSetBuilder](https://github.com/valdisiljuconoks/FeatureSwitch/blob/master/FeatureSwitch/FeatureSetBuilder.cs) by calling `Build()` instance method:

```
  var builder = new FeatureSetBuilder();
  builder.Build();
```

By calling `Build` method you are triggering auto-discovery of features in current application domain loaded assemblies. Auto-discovery will look for classes inheriting from `FeatureSwitch.BaseFeature` class. Those are assumed to be features.

## Is Feature Enabled?
After features have been discovered and set has been built you are able to check whether feature is enabled or not:

```
  var isEnabled = FeatureContext.IsEnabled<MySampleFeature>();
```

You can also use some of the `IsEnabled` overloads for other usages.

## Strategies
Whereas strategies are aspect in the library that is controlling either feature is enabled or disabled in the current circumstances. Strategy is an attribute you decorate your feature with:

```
  [FeatureSwitch.Strategies.AppSettings(Key = "MySampleFeatureKey")]
  public class MySampleFeature : BaseFeature
  {
  }
```

Currently FeatureSwitch release contains following built-in strategies:
* AlwaysFalse - by using this strategy your feature will be always disabled
* AlwaysTrue - this strategy will always make your feature shine
* AppSettings - key under `<appSettings>` element either in web.config or app.config will control this feature state
* HttpSession - if you need more permanent storage for your feature's state you can make use of Http session
* QueryString - if you will provide magic key in query string, feature may enable or disable.

## Multiple Strategies
Single feature can support more that one strategy. For instance:

```
  [AppSettings(Key = "StyleOptimizationDisabled")]
  [QueryString(Key = "DisableStyleOptimization", Order = 1)]
  public class StyleOptimizationDisabled : BaseFeature
  {
  }
```

this feature is controlled by `<appSettings>` element key in web.config file and then also by query string. It means that feature is disabled by `AppSettings` strategy but enabled by `QueryString` strategy (providing proper query string) - feature is eventually enabled.
In order to attach multiple strategies to single feature you need to define order for the strategy using `Order` property. FetureSetBuilder will fail if it founds multiple strategies with undefined or equal order.
When feature is asked for enabled state all strategies in ascending order are used to query for feature enabled state. If any of those return `true` feature is enabled.

## Writable Strategies
Sometimes it's required to store feature enabled state in more permanent storage (session, cache, etc). For this reason you can use any of `FeatureSwitch.Strategies.BaseStrategyImpl` implementations.
Currently available writeable strategies:
* HttpSession - using this strategy you can enable or disable feature and it will remain in this state as long as session is alive

## Advanced Context Construction Cases
At moment of current release of FeatureSwitch library we do have dependency on [structuremap](https://www.nuget.org/packages/structuremap/) in order to keep track of strategies and dependencies and also to support [constructor injection](http://en.wikipedia.org/wiki/Dependency_injection) for custom strategies if needed (this is subject for refactoring).
Using dependency injection you are able to instruct `FeatureSetBuilder` how to construct context and which strategies to use and how to initialize them.

### Disable Auto-Discovery
Sometimes it's required to disable auto-discovery and use only predefined list of features.
This can be accomplished using `action` parameter of `Build()` method:

```
  var builder = new FeatureSetBuilder();
  builder.Build(ctx => ctx.AddFeature<MySampleFeature>());
```

This will add only `MySampleFeature` to the context and will not perform auto-discovery of features.


### Custom Strategy With Dependency
Let's assume that we have a fancy strategy that is depended on even more fancier service to work properly:

```
  public class StrategyWithConstructorParameterReader : BaseStrategyImpl
  {
      public StrategyWithConstructorParameterReader(ISampleInjectedInterface sample)
      {
          ...
```

So to properly configure dependency composition root we need to configure `FeatureSetBuilder` to inject `ISampleInjectedInterface` properly.
To do so you can use `dependencyConfiguration` parameter of `Build()` method:

```
  builder.Build(dependencyConfiguration: ex => ex.For<ISampleInjectedInterface>().Use<SampleInjectedInterface>());
```

## More Information
[More information](https://github.com/valdisiljuconoks/FeatureSwitch/wiki/Extending-FeatureSwitch-library) on extending FeatureSwitch library.
