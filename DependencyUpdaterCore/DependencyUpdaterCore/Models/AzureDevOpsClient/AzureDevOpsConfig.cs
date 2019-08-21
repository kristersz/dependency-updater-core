namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    internal class AzureDevOpsConfig : IAzureDevOpsConfig
    {
        public string Project { get; set; }

        public string Repository { get; set; }

        public string BaseUrl { get; set; }

        public string Token { get; set; }
    }
}
