namespace DependencyUpdaterCore.Models
{
    public class UpdatedCsProjFile
    {
        public string FileContent { get; set; }

        public string PackageId { get; set; }

        public string UpdatedVersion { get; set; }
    }
}
