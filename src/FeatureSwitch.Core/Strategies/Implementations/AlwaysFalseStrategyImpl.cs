namespace FeatureSwitch.Strategies.Implementations
{
    public class AlwaysFalseStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return false;
        }
    }
}
