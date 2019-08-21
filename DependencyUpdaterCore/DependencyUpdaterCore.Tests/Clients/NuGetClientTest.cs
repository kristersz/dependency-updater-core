using DependencyUpdaterCore.Clients.NuGet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Tests.Clients
{
    [TestClass]
    public class NuGetClientTest
    {
        [TestMethod]
        public async Task SearchUpdates_SamplePackage_ReturnsVersion()
        {
            var client = new NuGetClient();

            await client.SearchUpdates();
        }
    }
}
