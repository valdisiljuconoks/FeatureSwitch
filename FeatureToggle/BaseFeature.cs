namespace FeatureToggle
{
    public class BaseFeature
    {
        private bool canModify;
        private bool isEnabled;

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
        }
        public bool CanModify
        {
            get
            {
                return this.canModify;
            }
        }

        public string Name { get; internal set; }

        internal void ChangeEnabledState(bool enabled)
        {
            this.isEnabled = enabled;
        }

        internal void ChangeModifiableState(bool modifiable)
        {
            this.canModify = modifiable;
        }
    }
}
