using System.Threading.Tasks;

namespace DependencyUpdaterCore
{
    public interface IDependencyUpdater
    {
        Task UpdateDependencies();
    }
}
