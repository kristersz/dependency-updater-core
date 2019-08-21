using System.IO;

namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    public interface ICsProjResponse
    {
        Stream File { get; }
        string FileRelativePath { get; }
    }
}
