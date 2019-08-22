using DependencyUpdaterCore.Clients.AzureDevOps;
using DependencyUpdaterCore.Features.FileFetching;
using DependencyUpdaterCore.Features.FileParsing;
using DependencyUpdaterCore.Features.FileUpdating;
using DependencyUpdaterCore.Features.PullRequestCreation;
using DependencyUpdaterCore.Features.UpdateChecking;
using DependencyUpdaterCore.Models;
using DependencyUpdaterCore.Models.AzureDevOpsClient;
using System;
using System.Threading.Tasks;

namespace DependencyUpdaterCore
{
    public class DependencyUpdater : IDependencyUpdater
    {
        private readonly ICSharpProjectFileFetcher _fileFetcher;
        private readonly ICSharpProjectParser _fileParser;
        private readonly ICSharpProjectFileUpdater _fileUpdater;
        private readonly IDependencyUpdateChecker _updateChecker;
        private readonly IPullRequestCreator _pullRequestCreator;

        public DependencyUpdater(IAzureDevOpsConfig config)
        {
            var client = new AzureDevOpsClient(config);

            _pullRequestCreator = new PullRequestCreator(client);
            _fileFetcher = new CSharpProjectFileFetcher(client);
            _updateChecker = new DependencyUpdateChecker();
            _fileUpdater = new CSharpProjectFileUpdater();
            _fileParser = new CSharpProjectParser();
        }

        public async Task UpdateDependencies(IUpdateCheckingConfig updateCheckingConfig)
        {
            try
            {
                var files = await _fileFetcher.GetProjectDependencyFileAsync();

                foreach (var file in files)
                {
                    var dependencyInfo = _fileParser.GetCsProjDependencyInfo(file.File);

                    var latestPackageVersions = await _updateChecker.CheckForUpdates(dependencyInfo.PackageInfos, updateCheckingConfig);

                    var updatedFiles = _fileUpdater.UpdateCsProjFile(dependencyInfo, latestPackageVersions);

                    foreach (var updatedFile in updatedFiles)
                    {
                        await _pullRequestCreator.CreatePullRequest(
                            new CreatePullRequestInfo
                            {
                                Content = updatedFile.FileContent,
                                Path = file.FileRelativePath,
                                PackageId = updatedFile.PackageId
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
