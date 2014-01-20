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
* [EPiServer integration](https://github.com/valdisiljuconoks/FeatureSwitch/wiki/EPiServer-Integration) - Module to integrate into [EPiServer CMS platform](http://www.episerver.com) for easier access to UI Control Panel

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

## Where do I get packages?
All packages are available on NuGet feeds:
* [Core library](https://www.nuget.org/packages/FeatureSwitch/)
* [Asp.Net Mvc Integration](https://www.nuget.org/packages/FeatureSwitch.AspNet.Mvc/)
* [Asp.Net Mvc 5 Integration / Owin](https://www.nuget.org/packages/FeatureSwitch.AspNet.Mvc5/)
* [Web Optimization pack](https://www.nuget.org/packages/FeatureSwitch.Web.Optimization/)
* [EPiServer integration](http://nuget.episerver.com/en/OtherPages/Package/?packageId=FeatureSwitch.EPiServer)

## More Information
[More information](https://github.com/valdisiljuconoks/FeatureSwitch/wiki/Extending-FeatureSwitch-library) on extending FeatureSwitch library.
