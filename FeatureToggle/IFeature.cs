namespace FeatureToggle
{
    public interface IFeature
    {
        bool IsEnabled { get; }
    }
}
