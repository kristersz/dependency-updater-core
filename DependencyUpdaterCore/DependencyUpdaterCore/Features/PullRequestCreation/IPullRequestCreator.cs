using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.PullRequestCreation
{
    public interface IPullRequestCreator
    {
        Task CreatePullRequest(string content);
    }
}
