using System.Linq;
using EPiServer.Data.Dynamic;
using FeatureSwitch.Strategies;

namespace FeatureSwitch.EPiServer.Strategies
{
    public class EPiServerDatabaseStrategyImpl : BaseStrategyImpl
    {
        public override bool Read()
        {
            var store = typeof(FeatureState).GetStore();
            var definition = store.Items<FeatureState>().FirstOrDefault(d => d.Name == Context.Key);
            return definition != null && definition.Enabled;
        }

        public override void Write(bool state)
        {
            var store = typeof(FeatureState).GetStore();

            var definition = store.Items<FeatureState>().FirstOrDefault(d => d.Name == Context.Key) ?? 
                new FeatureState() { Name = Context.Key };

            definition.Enabled = state;
            store.Save(definition);
        }
    }
}
