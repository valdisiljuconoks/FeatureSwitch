using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureSwitch.Strategies.Implementations;
using Glimpse.Core.Extensibility;
using Glimpse.Core.ResourceResult;
using Glimpse.Core.Tab.Assist;

namespace FeatureSwitch.Glimpse
{
    public class QueryClientScript : IStaticClientScript
    {
        public ScriptOrder Order
        {
            get
            {
                return ScriptOrder.IncludeAfterClientInterfaceScript;
            }
        }

        public string GetUri(string version)
        {
            return "/FeatureSwitchConfig.js";
        }
    }

    public class FeatureSwitchResource : IResource
    {
        public IResourceResult Execute(IResourceContext context)
        {
            var p = context.Parameters;

            string featureName;
            if (!p.TryGetValue("featurename", out featureName))
            {
                throw new ArgumentException("Missing name of the feature");
            }

            string value;
            if (!p.TryGetValue("val", out value))
            {
                throw new ArgumentException("Missing new value for the feature");
            }

            if (string.IsNullOrEmpty(featureName) || string.IsNullOrEmpty(value))
            {
                return GenerateResponse();
            }

            var newValue = Boolean.Parse(value);
            if (newValue)
            {
                FeatureContext.Enable(featureName);
            }
            else
            {
                FeatureContext.Disable(featureName);
            }

            return GenerateResponse();
        }

        public string Name
        {
            get
            {
                return "featureswitch_config";
            }
        }
        public IEnumerable<ResourceParameterMetadata> Parameters
        {
            get
            {
                return new[] { new ResourceParameterMetadata("featurename"), new ResourceParameterMetadata("val") };
            }
        }

        private static IResourceResult GenerateResponse()
        {
            var features = FeatureContext.GetFeatures();
            var vm = features.Select(f => new
            {
                Enabled = FeatureContext.IsEnabled(f),
                f.Name,
                f.CanModify,
                f.GetType().FullName
            }).ToList();
            return new JsonResourceResult(vm);
        }
    }

    public class Tab : TabBase<HttpContextBase>, ITabLayout, IKey
    {
        private static readonly object _layout = TabLayout.Create().Row(r =>
        {
            r.Cell("enabled").WidthInPixels(150);
            r.Cell("name");
        }).Build();
        public override string Name
        {
            get
            {
                return "Feature Switch";
            }
        }
        public override RuntimeEvent ExecuteOn
        {
            get
            {
                // we need check if Session strategy is used - then plug-in should be run once session access event fired
                return FeatureContext.IsStrategyEnabled<HttpSessionStrategyImpl>() ? RuntimeEvent.EndSessionAccess : RuntimeEvent.EndRequest;
            }
        }
        public string Key
        {
            get
            {
                return "glimpse_featureswitch";
            }
        }

        public object GetLayout()
        {
            return _layout;
        }

        public override object GetData(ITabContext context)
        {
            var features = FeatureContext.GetFeatures();
            var vm = features.Select(f => new { Enabled = FeatureContext.IsEnabled(f), f.Name }).ToList();
            return vm;
        }
    }
}
