using DependencyUpdaterCore.Clients.NuGet;
using DependencyUpdaterCore.Clients.ProGet;
using DependencyUpdaterCore.Models;
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

        public async Task<IList<IPackageInfo>> CheckForUpdates(IList<IPackageInfo> packages)
        {
            var result = new List<IPackageInfo>();

            foreach (var package in packages)
            {
                var nugetPackageVersion = await _nuGetClient.GetLatestPackageVersion(package.PackageId);

                if (!string.IsNullOrWhiteSpace(nugetPackageVersion))
                {
                    result.Add(new PackageInfo { PackageId = package.PackageId, Version = nugetPackageVersion });
                }
                else
                {
                    var progetPackageVersion = await _proGetClient.GetLatestPackageVersion(package.PackageId);

                    if (!string.IsNullOrWhiteSpace(progetPackageVersion))
                    {
                        result.Add(new PackageInfo { PackageId = package.PackageId, Version = progetPackageVersion });
                    }
                }
            }

            return result;
        }
    }
}
