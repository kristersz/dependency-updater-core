using DependencyUpdaterCore.Clients.NuGet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Tests.Clients
{
    [TestClass]
    public class NuGetClientTest
    {
        [TestMethod]
        public async Task GetLatestPackageVersion_BotModelsPackage_ReturnsLatestVersion()
        {
            var client = new NuGetClient();

            var versions = await client.GetPackageVersions("Newtonsoft.Json");

            Assert.IsNotNull(versions);
        }

        [TestMethod]
        public async Task GetLatestPackageVersion_NonExistingPackage_ReturnsNull()
        {
            var client = new NuGetClient();

            var versions = await client.GetPackageVersions("If.ClaimsForms.Bot.Models");

            Assert.IsNull(versions);
        }
    }
}
