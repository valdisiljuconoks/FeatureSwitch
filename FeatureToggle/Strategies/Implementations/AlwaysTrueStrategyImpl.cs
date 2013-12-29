namespace FeatureToggle.Strategies.Implementations
{
    public class AlwaysTrueStrategyImpl : BaseStrategyReaderImpl
    {
        public override bool Read()
        {
            return true;
        }
    }
}
