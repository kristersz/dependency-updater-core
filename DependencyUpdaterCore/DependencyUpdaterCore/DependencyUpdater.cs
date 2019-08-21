using DependencyUpdaterCore.Features.FileFetching;
using DependencyUpdaterCore.Features.FileParsing;
using DependencyUpdaterCore.Features.FileUpdating;
using DependencyUpdaterCore.Features.UpdateChecking;
using DependencyUpdaterCore.Models.AzureDevOpsClient;

namespace DependencyUpdaterCore
{
    public class DependencyUpdater : IDependencyUpdater
    {
        private readonly ICSharpProjectFileFetcher _fileFetcher;
        private readonly ICSharpProjectParser _fileParser;
        private readonly ICSharpProjectFileUpdater _fileUpdater;
        private readonly IDependencyUpdateChecker _updateChecker;

        public DependencyUpdater(
            ICSharpProjectFileFetcher fileFetcher,
            ICSharpProjectParser fileParser,
            ICSharpProjectFileUpdater fileUpdater,
            IDependencyUpdateChecker updateChecker)
        {
            _fileFetcher = fileFetcher;
            _fileParser = fileParser;
            _fileUpdater = fileUpdater;
            _updateChecker = updateChecker;
        }

        public void UpdateDependencies(IAzureDevOpsConfig request)
        {
            // fetch dependency files

            // parse files to get a list of dependencies and their versions

            // check dependencies for updates

            // update files with latest dependency versions

            // create Pull Requests
        }
    }
}
