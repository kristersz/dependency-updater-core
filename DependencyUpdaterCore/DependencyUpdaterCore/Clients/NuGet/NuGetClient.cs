using System.Net.Http;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.NuGet
{
    public class NuGetClient : INuGetClient
    {
        public async Task SearchUpdates()
        {
            using (var httpClient = new HttpClient())
            {
                var packageId = "If.ClaimsForms.Bot.Models";

                var requestUri = $"http://cloudgallery.ifext.biz/nuget/If-Nuget/{packageId}/index.json";
                var response = await httpClient.GetAsync(requestUri);
            }
        }
    }
}
