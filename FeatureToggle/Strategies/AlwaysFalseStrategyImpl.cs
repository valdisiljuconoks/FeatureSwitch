namespace FeatureToggle.Strategies
{
    public class AlwaysFalseStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return false;
        }
    }
}
