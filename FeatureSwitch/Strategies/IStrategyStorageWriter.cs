namespace FeatureSwitch.Strategies
{
    public interface IStrategyStorageWriter : IStrategy
    {
        void Write(bool state);
    }
}
