using DependencyUpdaterCore.Models.AzureDevOpsClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.AzureDevOps
{
    public interface IAzureDevOpsClient
    {
        Task<IList<ICsProjResponse>> GetProjectDependencyFileAsync();

        Task CreateCommitAsync();

        Task CreatePullRequestAsync();
    }
}
