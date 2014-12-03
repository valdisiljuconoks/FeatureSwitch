using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace FeatureSwitch.EPiServer.Strategies
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    internal class FeatureState : IDynamicData
    {
        public Identity Id { get; set; }
        [EPiServerDataIndex]
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
