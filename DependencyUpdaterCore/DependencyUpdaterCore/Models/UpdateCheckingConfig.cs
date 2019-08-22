namespace DependencyUpdaterCore.Models
{
    public class UpdateCheckingConfig : IUpdateCheckingConfig
    {
        public bool IncludePrereleaseVersions { get; set; }

        public bool UpdateMajorVersions { get; set; }
    }
}
