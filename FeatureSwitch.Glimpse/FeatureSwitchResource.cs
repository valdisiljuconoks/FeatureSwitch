using System;
using System.Collections.Generic;
using System.Linq;
using Glimpse.Core.Extensibility;
using Glimpse.Core.ResourceResult;

namespace FeatureSwitch.Glimpse
{
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
}
