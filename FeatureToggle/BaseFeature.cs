namespace FeatureToggle
{
    public class BaseFeature : IFeature
    {
        private bool isEnabled;

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
        }

        internal void ChangeState(bool enabled)
        {
            this.isEnabled = enabled;
        }
    }
}
