namespace FeatureToggle.Strategies.Implementations
{
    public class AlwaysFalseStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return false;
        }
    }
}
