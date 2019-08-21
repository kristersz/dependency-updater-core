using DependencyUpdaterCore.Clients.NuGet;
using DependencyUpdaterCore.Clients.ProGet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.UpdateChecking
{
    public class DependencyUpdateChecker : IDependencyUpdateChecker
    {
        private readonly INuGetClient _nuGetClient;
        private readonly IProGetClient _proGetClient;

        public DependencyUpdateChecker()
        {
            _nuGetClient = new NuGetClient();
            _proGetClient = new ProGetClient();
        }

        public async Task<List<Tuple<string, string>>> CheckForUpdates(List<string> packageIds)
        {
            var result = new List<Tuple<string, string>>();

            foreach (var packageId in packageIds)
            {
                var nugetPackageVersion = await _nuGetClient.GetLatestPackageVersion(packageId);

                if (!string.IsNullOrWhiteSpace(nugetPackageVersion))
                {
                    result.Add(new Tuple<string, string>(packageId, nugetPackageVersion));
                }
                else
                {
                    var progetPackageVersion = await _proGetClient.GetLatestPackageVersion(packageId);

                    if (!string.IsNullOrWhiteSpace(progetPackageVersion))
                    {
                        result.Add(new Tuple<string, string>(packageId, progetPackageVersion));
                    }
                }
            }

            return result;
        }
    }
}
