namespace FeatureSwitch.Strategies
{
    public interface IStrategyStorageWriter : IStrategyStorageReader
    {
        void Write(bool state);
    }
}
