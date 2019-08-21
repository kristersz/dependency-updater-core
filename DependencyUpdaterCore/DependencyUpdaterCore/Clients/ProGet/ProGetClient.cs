using DependencyUpdaterCore.Features.UpdateChecking;
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

        public async Task<string> GetLatestPackageVersion(string packageId)
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

                var sortedByVersion = allPublishedPackages
                    .Where(c => !c.Version.Contains("-"))
                    .OrderByDescending(p => p.Version, new VersionComparer())
                    .ToList();

                return sortedByVersion.FirstOrDefault()?.Version;
            }
        }

        private class PackageData
        {
            public string Version { get; set; }
        }
    }
}
