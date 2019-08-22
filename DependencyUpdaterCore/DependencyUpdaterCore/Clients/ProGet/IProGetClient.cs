using DependencyUpdaterCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.ProGet
{
    public interface IProGetClient
    {
        Task<IList<IVersionable>> GetPackageVersions(string packageId);
    }
}
