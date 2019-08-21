namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface IAzureDevOpsConfig
    {
        string Project { get; }
        string Repository { get; }
        string BaseUrl { get; }
        string Token { get; }
    }
}
