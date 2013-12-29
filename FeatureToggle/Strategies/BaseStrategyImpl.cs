using FeatureToggle.Strategies.Implementations;

namespace FeatureToggle.Strategies
{
    public abstract class BaseStrategyImpl : BaseStrategyReaderImpl, IStrategyStorageWriter
    {
        public abstract void Write(bool state);
    }
}
