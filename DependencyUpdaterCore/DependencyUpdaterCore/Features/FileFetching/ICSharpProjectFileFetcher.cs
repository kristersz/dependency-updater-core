using DependencyUpdaterCore.Models.AzureDevOpsClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.FileFetching
{
    public interface ICSharpProjectFileFetcher
    {
        Task<IList<ICsProjResponse>> GetProjectDependencyFileAsync();
    }
}
