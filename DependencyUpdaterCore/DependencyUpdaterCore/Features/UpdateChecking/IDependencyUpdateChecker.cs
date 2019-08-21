using DependencyUpdaterCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.UpdateChecking
{
    public interface IDependencyUpdateChecker
    {
        Task<IList<IPackageInfo>> CheckForUpdates(IList<IPackageInfo> packages);
    }
}
