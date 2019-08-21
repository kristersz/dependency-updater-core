using DependencyUpdaterCore.Clients.AzureDevOps;
using DependencyUpdaterCore.Models.AzureDevOpsClient;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.PullRequestCreation
{
    internal class PullRequestCreator : IPullRequestCreator
    {
        private readonly IAzureDevOpsClient _azureDevOpsClient;

        public PullRequestCreator(IAzureDevOpsClient azureDevOpsClient)
        {
            _azureDevOpsClient = azureDevOpsClient;
        }

        public async Task CreatePullRequest(string content)
        {
            var commitRequest = new CommitInfo
            {
                Comment = "Update commit",
                Content = content,
                FileRelativePath = "meeded"
            };

            var newBranch = await _azureDevOpsClient.CreateCommitAsync(commitRequest);

            var request = new PullRequestInfo
            {
                Description = "Automatic updates from updater.",
                Title = "needed",
                SourceBranch = newBranch
            };

            await _azureDevOpsClient.CreatePullRequestAsync(request);
        }
    }
}
