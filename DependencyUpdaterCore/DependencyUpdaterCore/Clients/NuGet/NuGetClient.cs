using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.NuGet
{
    public class NuGetClient : INuGetClient
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
                    .OrderByDescending(p => p.Version, new VersionComparer())
                    .ToList();

                return sortedByVersion.FirstOrDefault()?.Version;
            }
        }

        private class PackageData
        {
            public string Version { get; set; }
        }

        private class VersionComparer : IComparer<string>
        {
            public int Compare(string first, string second)
            {
                var versions = first.Split('.');
                var otherVersions = second.Split('.');

                var majorVersionComparison = CompareNumbers(versions[0], otherVersions[0]);

                if (majorVersionComparison != 0)
                {
                    return majorVersionComparison;
                }

                var minorVersionComparison = CompareNumbers(versions[1], otherVersions[1]);

                if (minorVersionComparison != 0)
                {
                    return minorVersionComparison;
                }

                return CompareNumbers(versions[2], otherVersions[2]);
            }

            private int CompareNumbers(string first, string second)
            {
                int.TryParse(first, out int firstInt);
                int.TryParse(second, out int secondInt);

                return firstInt.CompareTo(secondInt);
            }
        }
    }
}
