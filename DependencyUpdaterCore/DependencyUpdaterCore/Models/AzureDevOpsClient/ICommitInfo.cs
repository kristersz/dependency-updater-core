namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface ICommitInfo
    {
        string FileRelativePath { get; }
        string Comment { get; }
        string Content { get; }
    }
}