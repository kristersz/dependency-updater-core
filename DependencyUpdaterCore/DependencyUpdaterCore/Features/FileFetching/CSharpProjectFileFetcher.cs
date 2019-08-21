using DependencyUpdaterCore.Clients.AzureDevOps;
using DependencyUpdaterCore.Models.AzureDevOpsClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.FileFetching
{
    public class CSharpProjectFileFetcher : ICSharpProjectFileFetcher
    {
        private readonly IAzureDevOpsClient _azureDevOpsClient;

        public CSharpProjectFileFetcher(IAzureDevOpsClient azureDevOpsClient)
        {
            _azureDevOpsClient = azureDevOpsClient;
        }

        public async Task<IList<ICsProjResponse>> GetProjectDependencyFileAsync()
        {
            return await _azureDevOpsClient.GetProjectDependencyFileAsync();
        }
    }
}
