namespace FeatureToggle.Strategies
{
    public interface IStrategyStorageWriter : IStrategy
    {
        void Write(bool state);
    }
}
