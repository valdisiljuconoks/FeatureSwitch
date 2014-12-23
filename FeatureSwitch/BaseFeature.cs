namespace FeatureSwitch
{
    public class BaseFeature
    {
        public bool CanModify { get; private set; }
        public bool IsProperlyConfigured { get; private set; }
        public string Name { get; internal set; }
        public virtual string GroupName
        {
            get
            {
                return string.Empty;
            }
        }

        internal void MarkAsMisConfigured(bool state)
        {
            IsProperlyConfigured = state;
        }

        internal void MarkAsModifiable()
        {
            CanModify = true;
        }
    }
}
