using FeatureSwitch.Strategies.Implementations;

namespace FeatureSwitch.Strategies
{
    public abstract class BaseStrategyImpl : BaseStrategyReaderImpl, IStrategyStorageWriter
    {
        public abstract void Write(bool state);
    }
}
