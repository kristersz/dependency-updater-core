namespace DependencyUpdaterCore.Models
{
    public interface IUpdateCheckingConfig
    {
        bool IncludePrereleaseVersions { get; set; }

        bool UpdateMajorVersions { get; set; }
    }
}
