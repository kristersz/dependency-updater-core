using DependencyUpdaterCore.Models.AzureDevOpsClient;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DependencyUpdaterCore.Tests")]
namespace DependencyUpdaterCore.Clients.AzureDevOps
{
    class AzureDevOpsClient : IAzureDevOpsClient
    {
        private readonly IAzureDevOpsConfig _config;

        public AzureDevOpsClient(IAzureDevOpsConfig config)
        {
            _config = config;
        }

        public async Task<string> CreateCommitAsync(ICommitInfo commitInfo)
        {
            try
            {
                var creds = new VssBasicCredential(string.Empty, _config.Token);
                var branchName = $"Updater_{DateTime.Now.Ticks}";

                using (var connection = new VssConnection(new Uri(_config.BaseUrl), creds))
                {
                    var gitClient = connection.GetClient<GitHttpClient>();

                    var items = await gitClient
                        .GetItemsAsync(
                            project: _config.Project,
                            repositoryId: _config.Repository
                        );

                    var commitId = items?.FirstOrDefault()?.CommitId;

                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(commitInfo.Content);

                    var result = await gitClient.CreatePushAsync(new GitPush
                    {
                        Commits = new List<GitCommitRef>
                        {
                            new GitCommitRef
                            {
                                Comment = commitInfo.Comment,
                                Changes = new List<GitChange>
                                {
                                    new GitChange
                                    {
                                        ChangeType = VersionControlChangeType.Edit,
                                        Item = new GitItem
                                        {
                                            Path = commitInfo.FileRelativePath
                                        },
                                        NewContent = new ItemContent
                                        {
                                            Content = Convert.ToBase64String(plainTextBytes),
                                            ContentType = ItemContentType.Base64Encoded
                                        }
                                    }
                                }
                            }
                        },
                        RefUpdates = new List<GitRefUpdate>
                        {
                            new GitRefUpdate
                            {
                                Name = $"refs/heads/{branchName}",
                                OldObjectId = commitId,
                            }
                        }
                    },
                    project: _config.Project,
                    repositoryId: _config.Repository
                    );
                }
                return branchName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreatePullRequestAsync(IPullRequestInfo pullRequestInfo)
        {
            try
            {
                var creds = new VssBasicCredential(string.Empty, _config.Token);

                using (var connection = new VssConnection(new Uri(_config.BaseUrl), creds))
                {
                    var gitClient = connection.GetClient<GitHttpClient>();

                    var result = await gitClient.CreatePullRequestAsync(new GitPullRequest
                    {
                        SourceRefName = $"refs/heads/{pullRequestInfo.SourceBranch}",
                        TargetRefName = "refs/heads/master",
                        Title = pullRequestInfo.Title,
                        Description = pullRequestInfo.Description,
                    },
                    project: _config.Project,
                    repositoryId: _config.Repository
                    );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IList<ICsProjResponse>> GetProjectDependencyFileAsync()
        {
            try
            {
                var creds = new VssBasicCredential(string.Empty, _config.Token);

                using (var connection = new VssConnection(new Uri(_config.BaseUrl), creds))
                {
                    var gitClient = connection.GetClient<GitHttpClient>();

                    var items = await gitClient
                        .GetItemsAsync(
                            project: _config.Project,
                            repositoryId: _config.Repository
                        );

                    var hash = items?.FirstOrDefault()?.ObjectId;

                    var repositoryTree = await gitClient.GetTreeAsync(
                        project: _config.Project,
                        repositoryId: _config.Repository,
                        sha1: hash
                        );

                    var solutionTrees = repositoryTree.TreeEntries
                        .Where(w => w.GitObjectType == GitObjectType.Tree);

                    var result = new List<ICsProjResponse>();

                    foreach (var tree in solutionTrees)
                    {
                        var innerTree = await gitClient.GetTreeAsync(
                            project: _config.Project,
                            repositoryId: _config.Repository,
                            sha1: tree.ObjectId
                            );

                        var csprojEntries = innerTree.TreeEntries
                            .Where(w => w.RelativePath.EndsWith(".csproj"));

                        if (csprojEntries.Count() > 1)
                        {
                            throw new Exception("wtf? multiple .csproj files?");
                        }

                        var csprojItem = csprojEntries.FirstOrDefault();

                        var csprojStream = await gitClient.GetBlobContentAsync(
                            repositoryId: _config.Repository,
                            project: _config.Project,
                            sha1: csprojItem.ObjectId
                            );

                        using (csprojStream)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                csprojStream.CopyTo(ms);
                                var array = ms.ToArray();
                                result.Add(new CsProjResponse
                                {
                                    File = array,
                                    FileRelativePath = $"/{tree.RelativePath}/{csprojItem.RelativePath}"
                                });
                            }
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}