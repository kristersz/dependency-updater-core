namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface ICsProjResponse
    {
        byte[] File { get; }
        string FileRelativePath { get; }
    }
}
