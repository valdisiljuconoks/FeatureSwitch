using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace FeatureSwitch.EPiServer.Strategies
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    internal class FeatureState : IDynamicData
    {
        [EPiServerDataIndex]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public Identity Id { get; set; }
    }
}
