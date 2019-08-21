namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface ICsProjResponse
    {
        byte[] File { get; }
        string FileName { get; }
    }
}
