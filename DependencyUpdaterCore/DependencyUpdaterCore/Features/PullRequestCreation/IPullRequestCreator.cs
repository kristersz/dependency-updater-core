using DependencyUpdaterCore.Models.AzureDevOpsClient;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.PullRequestCreation
{
    public interface IPullRequestCreator
    {
        Task CreatePullRequest(ICreatePullRequestInfo createPullRequestInfo);
    }
}
