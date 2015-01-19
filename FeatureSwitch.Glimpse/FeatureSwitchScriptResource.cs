using Glimpse.Core.Extensibility;
using Glimpse.Core.Resource;

namespace FeatureSwitch.Glimpse
{
    public class FeatureSwitchScriptResource : FileResource, IKey
    {
        internal const string InternalName = "glimpse_featureswitch_script";

        public FeatureSwitchScriptResource()
        {
            Name = InternalName;
            GlimpseClientEmbeddedResourceInfo = new EmbeddedResourceInfo(GetType().Assembly,
                                                                         "FeatureSwitch.Glimpse.FeatureSwitchConfigScript.min.js",
                                                                         "application/x-javascript");
        }

        public EmbeddedResourceInfo GlimpseClientEmbeddedResourceInfo { get; set; }

        public string Key
        {
            get
            {
                return Name;
            }
        }

        protected override EmbeddedResourceInfo GetEmbeddedResourceInfo(IResourceContext context)
        {
            return GlimpseClientEmbeddedResourceInfo;
        }
    }
}
