using DependencyUpdaterCore.Models;
using System.Threading.Tasks;

namespace DependencyUpdaterCore
{
    public interface IDependencyUpdater
    {
        Task UpdateDependencies(IUpdateCheckingConfig updateCheckingConfig);
    }
}
