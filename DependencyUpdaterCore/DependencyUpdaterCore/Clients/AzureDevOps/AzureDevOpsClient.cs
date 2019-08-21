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
        public async Task<IList<ICsProjResponse>> GetProjectDependencyFileAsync(ICsProjRequest request)
        {
            try
            {
                var creds = new VssBasicCredential(string.Empty, request.Token);

                var connection = new VssConnection(new Uri(request.BaseUrl), creds);

                var gitClient = connection.GetClient<GitHttpClient>();

                var items = await gitClient
                    .GetItemsAsync(
                        project: request.Project,
                        repositoryId: request.Repository
                    );

                var hash = items?.FirstOrDefault()?.ObjectId;

                var repositoryTree = await gitClient.GetTreeAsync(
                    project: request.Project,
                    repositoryId: request.Repository,
                    sha1: hash
                    );

                var solutionTrees = repositoryTree.TreeEntries
                    .Where(w => w.GitObjectType == GitObjectType.Tree);

                var result = new List<ICsProjResponse>();

                foreach (var tree in solutionTrees)
                {
                    var innerTree = await gitClient.GetTreeAsync(
                        project: request.Project,
                        repositoryId: request.Repository,
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
                        repositoryId: request.Repository,
                        project: request.Project,
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
                                FileName = csprojItem.RelativePath
                            });
                        }
                    }   
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}