using DependencyUpdaterCore.Clients.ProGet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Tests.Clients
{
    [TestClass]
    public class ProGetClientTest
    {
        [TestMethod]
        public async Task GetLatestPackageVersion_BotModelsPackage_ReturnsLatestVersion()
        {
            var client = new ProGetClient();

            var latestVersion = await client.GetLatestPackageVersion("If.ClaimsForms.Bot.Models");

            Assert.AreEqual("1.0.65", latestVersion);
        }

        [TestMethod]
        public async Task GetLatestPackageVersion_NonExistingPackage_ReturnsNull()
        {
            var client = new ProGetClient();

            var latestVersion = await client.GetLatestPackageVersion("Newtonsoft.Json");

            Assert.IsNull(latestVersion);
        }
    }
}
