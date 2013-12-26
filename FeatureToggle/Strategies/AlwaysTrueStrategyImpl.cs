namespace FeatureToggle.Strategies
{
    public class AlwaysTrueStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return true;
        }
    }
}
