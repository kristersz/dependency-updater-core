namespace DependencyUpdaterCore.Models.AzureDevOpsClient
{
    internal class CommitInfo : ICommitInfo
    {
        public string FileRelativePath { get; set; }

        public string Comment { get; set; }

        public string Content { get; set; }
    }
}
