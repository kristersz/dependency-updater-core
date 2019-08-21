using DependencyUpdaterCore.Clients.AzureDevOps;
using DependencyUpdaterCore.Models.AzureDevOpsClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Tests.Clients
{
    [TestClass]
    public class AzureDevOpsClientTests
    {
        private readonly AzureDevOpsConfig _config = new AzureDevOpsConfig
            {
                BaseUrl = "https://dev.azure.com/if-it",
                Token = "vzj4iyh7e64lcif6jtlv5gwz3ka5hwhcwy5pqjcij3j7rdcbxbba",
                Project = "claims-private",
                Repository = "riga-claims-dashboard"
            };

    [TestMethod]
        public async Task GetProjectDependencyFileAsync()
        {
              var result = await new AzureDevOpsClient(_config)
                .GetProjectDependencyFileAsync();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task CreateCommitAsync()
        {
            await new AzureDevOpsClient(_config)
                .CreateCommitAsync();
        }

        [TestMethod]
        public async Task CreatePullRequestAsync()
        {
            await new AzureDevOpsClient(_config)
                .CreatePullRequestAsync();
        }
    }
}
