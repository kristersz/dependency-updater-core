﻿using DependencyUpdaterCore.Features.UpdateChecking;
using DependencyUpdaterCore.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.NuGet
{
    public class NuGetClient : INuGetClient
    {
        private readonly string _baseUrl = "https://api.nuget.org/v3/registration3";

        public async Task<IList<IVersionable>> GetPackageVersions(string packageId)
        {
            using (var httpClient = new HttpClient())
            {
                var requestUri = $"{_baseUrl}/{packageId.ToLowerInvariant()}/index.json";

                var response = await httpClient.GetAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();

                var packageMetadata = JsonConvert.DeserializeObject<NuGetPackageMetadata>(content);

                var catalogEntries = packageMetadata.Items
                    .SelectMany(p => p.Items.Select(i => i.CatalogEntry))
                    .Cast<IVersionable>()
                    .ToList();

                return catalogEntries;
            }
        }

        private class NuGetPackageMetadata
        {
            public List<NuGetPackageCatalogPage> Items { get; set; }
        }

        private class NuGetPackageCatalogPage
        {
            public List<NugetPackage> Items { get; set; }
        }

        private class NugetPackage
        {
            public NugetPackageDetails CatalogEntry { get; set; }
        }

        private class NugetPackageDetails : IVersionable
        {
            public string Version { get; set; }
        }
    }
}
