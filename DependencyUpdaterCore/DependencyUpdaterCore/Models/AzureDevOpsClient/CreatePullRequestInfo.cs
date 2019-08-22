namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    internal class CreatePullRequestInfo : ICreatePullRequestInfo
    {
        public string Content { get; set; }

        public string Path { get; set; }

        public string PackageId { get; set; }

        public string UpdatedVersion { get; set; }
    }
}
