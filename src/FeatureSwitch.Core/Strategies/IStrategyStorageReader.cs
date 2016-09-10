namespace FeatureSwitch.Strategies
{
    public interface IStrategyStorageReader : IStrategy
    {
        bool Read();
    }
}
