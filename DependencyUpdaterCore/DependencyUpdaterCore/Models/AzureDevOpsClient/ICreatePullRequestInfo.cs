namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface ICreatePullRequestInfo
    {
        string Content { get; }
        string Path { get; }
        string PackageId { get; }
        string UpdatedVersion { get; }
    }
}