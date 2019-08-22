using DependencyUpdaterCore.Models.AzureDevOpsClient;

namespace SampleWebClient.Configuration
{
    public class AzureDevOpsConfig : IAzureDevOpsConfig
    {
        public string Project => "claims-private";

        public string Repository => "riga-claims-dashboard";

        public string BaseUrl => "https://dev.azure.com/if-it";

        public string Token => "vzj4iyh7e64lcif6jtlv5gwz3ka5hwhcwy5pqjcij3j7rdcbxbba";
    }
}
