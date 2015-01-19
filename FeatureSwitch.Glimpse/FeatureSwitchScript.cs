using Glimpse.Core.Extensibility;

namespace FeatureSwitch.Glimpse
{
    public sealed class FeatureSwitchScript : IDynamicClientScript
    {
        public ScriptOrder Order
        {
            get
            {
                return ScriptOrder.IncludeAfterClientInterfaceScript;
            }
        }

        public string GetResourceName()
        {
            return "glimpse_featureswitch_script";
        }
    }
}
