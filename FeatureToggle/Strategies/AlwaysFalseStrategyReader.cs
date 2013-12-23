namespace FeatureToggle.Strategies
{
    public class AlwaysFalseStrategyReader : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return false;
        }
    }
}
