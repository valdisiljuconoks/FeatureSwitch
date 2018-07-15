namespace FeatureSwitch
{
    public class BaseFeature
    {
        public bool CanModify { get; private set; }
        public bool IsProperlyConfigured { get; private set; }
        public string Name { get; internal set; }
        public virtual string GroupName => string.Empty;

        internal void MarkAsNotConfigured()
        {
            IsProperlyConfigured = false;
        }

        internal void MarkAsModifiable()
        {
            CanModify = true;
        }
    }
}
