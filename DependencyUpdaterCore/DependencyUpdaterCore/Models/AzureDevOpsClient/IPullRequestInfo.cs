namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface IPullRequestInfo
    {
        string SourceBranch { get; }
        string Title { get; }
        string Description { get; }
    }
}