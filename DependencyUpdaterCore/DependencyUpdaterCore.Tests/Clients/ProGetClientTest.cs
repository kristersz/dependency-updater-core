using DependencyUpdaterCore.Clients.ProGet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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

            var versions = await client.GetPackageVersions("If.ClaimsForms.Bot.Models");

            Assert.IsNotNull(versions);
        }

        [TestMethod]
        public async Task GetLatestPackageVersion_NonExistingPackage_ReturnsNull()
        {
            var client = new ProGetClient();

            var versions = await client.GetPackageVersions("Newtonsoft.Json");

            Assert.IsTrue(!versions.Any());
        }
    }
}
