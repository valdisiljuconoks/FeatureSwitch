namespace FeatureSwitch.Strategies.Implementations
{
    public class AlwaysTrueStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return true;
        }
    }
}
