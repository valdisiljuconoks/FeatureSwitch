namespace FeatureToggle
{
    public interface IFeature
    {
        bool IsEnabled { get; }
        bool CanModify { get; }
    }
}
