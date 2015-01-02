using System.Linq;
using System.Web;
using FeatureSwitch.Strategies.Implementations;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;

namespace FeatureSwitch.Glimpse
{
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
            var vm = features.Select(f => new
            {
                Enabled = FeatureContext.IsEnabled(f),
                Name = f.Name + " (" + f.GetType().FullName + ")"
            }).ToList();
            return vm;
        }
    }
}
