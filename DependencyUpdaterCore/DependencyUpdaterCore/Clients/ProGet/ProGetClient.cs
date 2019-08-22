using DependencyUpdaterCore.Features.UpdateChecking;
using DependencyUpdaterCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.ProGet
{
    public class ProGetClient : IProGetClient
    {
        private readonly string _baseUrl = "http://cloudgallery.ifext.biz/nuget/If-Nuget";

        public async Task<IList<IVersionable>> GetPackageVersions(string packageId)
        {
            using (var httpClient = new HttpClient())
            {
                var oDataQuery = $"$format=json&$filter=Id eq '{packageId}'&$select=Version";

                var requestUri = $"{_baseUrl}/Packages()?{oDataQuery}";

                var response = await httpClient.GetAsync(requestUri);

                var content = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(content);

                var results = json["d"]["results"];

                var allPublishedPackages = JsonConvert.DeserializeObject<List<PackageData>>(results.ToString());

                return allPublishedPackages
                    .Cast<IVersionable>()
                    .ToList();
            }
        }

        private class PackageData : IVersionable
        {
            public string Version { get; set; }
        }
    }
}
