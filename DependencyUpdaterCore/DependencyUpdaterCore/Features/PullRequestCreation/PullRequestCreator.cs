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

        public async Task CreatePullRequest(ICreatePullRequestInfo createPullRequestInfo)
        {
            var commitRequest = new CommitInfo
            {
                Comment = $"Updating package {createPullRequestInfo.PackageId} to v{createPullRequestInfo.UpdatedVersion}",
                Content = createPullRequestInfo.Content,
                FileRelativePath = createPullRequestInfo.Path
            };

            var newBranch = await _azureDevOpsClient.CreateCommitAsync(commitRequest);

            var request = new PullRequestInfo
            {
                Description = "Automatic updates from updater.",
                Title = $"Updating package {createPullRequestInfo.PackageId} to v{createPullRequestInfo.UpdatedVersion}",
                SourceBranch = newBranch
            };

            await _azureDevOpsClient.CreatePullRequestAsync(request);
        }
    }
}
