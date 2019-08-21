namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    class CsProjResponse : ICsProjResponse
    {
        public byte[] File { get; set; }
        public string FileRelativePath { get; set; }
    }
}
