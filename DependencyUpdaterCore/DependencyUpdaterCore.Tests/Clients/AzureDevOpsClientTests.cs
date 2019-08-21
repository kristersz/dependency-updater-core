using DependencyUpdaterCore.Clients.AzureDevOps;
using DependencyUpdaterCore.Models.AzureDevOpsClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Tests.Clients
{
    [TestClass]
    public class AzureDevOpsClientTests
    {
        [TestMethod]
        public async Task MyTestMethod()
        {
            var config = new AzureDevOpsConfig
            {
                BaseUrl = "https://dev.azure.com/if-it",
                Token = "vzj4iyh7e64lcif6jtlv5gwz3ka5hwhcwy5pqjcij3j7rdcbxbba",
                Project = "claims-private",
                Repository = "riga-claims-dashboard"
            };

            var result = await new AzureDevOpsClient(config)
                .GetProjectDependencyFileAsync();

            Assert.AreEqual(2, result.Count);
        }
    }
}
