using System;
using System.Collections.Generic;
using FeatureSwitch.Core.Strategies.Implementations;
using FeatureSwitch.Strategies;
using FeatureSwitch.Strategies.Implementations;

namespace FeatureSwitch.Core
{
    class Class1
    {
        private readonly Dictionary<Type, Type> _defaultImplementations = new Dictionary<Type, Type>
                                                                          {
                                                                              { typeof(AppSettings), typeof(AppSettingsStrategyImpl) },
                                                                              { typeof(HttpSession), typeof(HttpSessionStrategyImpl) },
                                                                              { typeof(QueryString), typeof(QueryStringStrategyImpl) }
                                                                          };
    }
}
