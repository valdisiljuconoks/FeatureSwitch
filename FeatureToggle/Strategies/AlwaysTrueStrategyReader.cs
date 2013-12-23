namespace FeatureToggle.Strategies
{
    public class AlwaysTrueStrategyReader : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return true;
        }
    }
}
