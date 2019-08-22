using DependencyUpdaterCore.Models.AzureDevOpsClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Tests
{
    [TestClass]
    public class DependencyUpdaterTest
    {
        [TestMethod]
        public async Task UpdateDependencies()
        {
            var config = new AzureDevOpsConfig
            {
                BaseUrl = "https://dev.azure.com/if-it",
                Token = "vzj4iyh7e64lcif6jtlv5gwz3ka5hwhcwy5pqjcij3j7rdcbxbba",
                Project = "claims-private",
                Repository = "riga-claims-dashboard"
            };

            var updater = new DependencyUpdater(config);

            await updater.UpdateDependencies();
        }
    }
}
