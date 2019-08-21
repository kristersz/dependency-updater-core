namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    internal class CreatePullRequestInfo : ICreatePullRequestInfo
    {
        public string Content { get; set; }

        public string Path { get; set; }
    }
}
