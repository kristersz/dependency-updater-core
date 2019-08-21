using System.IO;

namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    class CsProjResponse : ICsProjResponse
    {
        public Stream File { get; set; }
        public string FileRelativePath { get; set; }
    }
}
