using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.UpdateChecking
{
    public interface IDependencyUpdateChecker
    {
        Task<List<Tuple<string, string>>> CheckForUpdates(List<string> packageIds);
    }
}
