using DependencyUpdaterCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.NuGet
{
    public interface INuGetClient
    {
        Task<IList<IVersionable>> GetPackageVersions(string packageId);
    }
}
