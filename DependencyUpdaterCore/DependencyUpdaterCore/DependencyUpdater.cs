using DependencyUpdaterCore.Features.UpdateChecking;
using DependencyUpdaterCore.Models.AzureDevOpsClient;

namespace DependencyUpdaterCore
{
    public class DependencyUpdater : IDependencyUpdater
    {
        private readonly IDependencyUpdateChecker _updateChecker;

        public DependencyUpdater()
        {
            _updateChecker = new DependencyUpdateChecker();
        }

        public void UpdateDependencies(ICsProjRequest request)
        {
            // fetch dependency files

            // parse files to get a list of dependencies and their versions

            // check dependencies for updates

            // update files with latest dependency versions

            // create Pull Requests
        }
    }
}
