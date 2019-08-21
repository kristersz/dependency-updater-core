namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    internal class PullRequestInfo : IPullRequestInfo
    {
        public string SourceBranch { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
