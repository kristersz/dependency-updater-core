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

            var latestVersion = await client.GetLatestPackageVersion("If.ClaimsForms.Bot.Models");

            Assert.AreEqual("1.0.65", latestVersion);
        }
    }
}
