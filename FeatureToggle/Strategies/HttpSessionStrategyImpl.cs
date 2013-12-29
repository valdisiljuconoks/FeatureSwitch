namespace FeatureToggle.Strategies
{
    public class HttpSessionStrategyImpl : BaseStrategyImpl
    {
        public override bool Read()
        {
            return true;
        }

        public override void Write(bool state)
        {
        }
    }
}
