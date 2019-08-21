using DependencyUpdaterCore.Features.FileFetching;
using DependencyUpdaterCore.Features.FileParsing;
using DependencyUpdaterCore.Features.FileUpdating;
using DependencyUpdaterCore.Features.PullRequestCreation;
using DependencyUpdaterCore.Features.UpdateChecking;
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

        public DependencyUpdater(
            ICSharpProjectFileFetcher fileFetcher,
            ICSharpProjectParser fileParser,
            ICSharpProjectFileUpdater fileUpdater,
            IDependencyUpdateChecker updateChecker,
            IPullRequestCreator pullRequestCreator)
        {
            _fileFetcher = fileFetcher;
            _fileParser = fileParser;
            _fileUpdater = fileUpdater;
            _updateChecker = updateChecker;
            _pullRequestCreator = pullRequestCreator;
        }

        public async Task UpdateDependencies()
        {
            try
            {
                var files = await _fileFetcher.GetProjectDependencyFileAsync();

                foreach (var file in files)
                {
                    var dependencyInfo = _fileParser.GetCsProjDependencyInfo(file.File);

                    var latestPackageVersions = await _updateChecker.CheckForUpdates(dependencyInfo.PackageInfos);

                    var updatedFiles = _fileUpdater.UpdateCsProjFile(dependencyInfo, latestPackageVersions);

                    foreach (var updatedFile in updatedFiles)
                    {
                        await _pullRequestCreator.CreatePullRequest(
                            new CreatePullRequestInfo
                            {
                                Content = updatedFile,
                                Path = file.FileRelativePath
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
