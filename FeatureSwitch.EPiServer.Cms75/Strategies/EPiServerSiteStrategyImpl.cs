using System;
using System.Linq;
using EPiServer.Web;
using FeatureSwitch.Strategies.Implementations;

namespace FeatureSwitch.EPiServer.Strategies
{
    public class EPiServerSiteStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            var sites = Context.Key.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries);
            return sites.Contains(SiteDefinition.Current.Name, StringComparer.CurrentCultureIgnoreCase);
        }
    }
}
