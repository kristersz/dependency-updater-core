using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.ProGet
{
    public interface IProGetClient
    {
        Task<string> GetLatestPackageVersion(string packageId);
    }
}
