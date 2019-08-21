namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface ICsProjRequest
    {
        string Project { get; }
        string Repository { get; }
        string BaseUrl { get; }
        string Token { get; }
    }
}
