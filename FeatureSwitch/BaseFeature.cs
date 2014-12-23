namespace FeatureSwitch
{
    public class BaseFeature
    {
        public bool CanModify { get; private set; }
        public bool IsProperlyConfigured { get; private set; }
        public string Name { get; internal set; }

        internal void ChangeIsProperlyConfiguredState(bool state)
        {
            IsProperlyConfigured = state;
        }

        internal void ChangeModifiableState(bool modifiable)
        {
            CanModify = modifiable;
        }

        public virtual string GroupName
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
