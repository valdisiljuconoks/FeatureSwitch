namespace FeatureSwitch
{
    public class BaseFeature
    {
        private bool canModify;
        private bool isProperlyConfigured;

        public bool CanModify
        {
            get
            {
                return this.canModify;
            }
        }

        public bool IsProperlyConfigured
        {
            get
            {
                return this.isProperlyConfigured;
            }
        }

        public string Name { get; internal set; }

        internal void ChangeIsProperlyConfiguredState(bool state)
        {
            this.isProperlyConfigured = state;
        }

        internal void ChangeModifiableState(bool modifiable)
        {
            this.canModify = modifiable;
        }
    }
}
