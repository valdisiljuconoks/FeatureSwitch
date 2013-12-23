namespace FeatureToggle.Strategies
{
    public interface IStrategyStorageReader : IStrategy
    {
        bool Read();
    }
}
