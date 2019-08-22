using DependencyUpdaterCore.Clients.NuGet;
using DependencyUpdaterCore.Clients.ProGet;
using DependencyUpdaterCore.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<IPackageInfo>> CheckForUpdates(IList<IPackageInfo> packages, IUpdateCheckingConfig config)
        {
            var result = new List<IPackageInfo>();

            foreach (var package in packages)
            {
                var packageId = package.PackageId;
                var currentVersion = new Version(package.Version);

                var availableVersions = await _nuGetClient.GetPackageVersions(packageId);

                if (availableVersions == null || !availableVersions.Any())
                {
                    availableVersions = await _proGetClient.GetPackageVersions(packageId);
                }

                var versions = availableVersions.Select(v => new Version(v.Version)).ToList();

                if (config.IncludePrereleaseVersions)
                {
                    versions = versions
                        .Where(v => !string.IsNullOrWhiteSpace(v.Prerelease))
                        .ToList();
                }

                if (!config.IncludePrereleaseVersions)
                {
                    versions = versions
                        .Where(v => string.IsNullOrWhiteSpace(v.Prerelease))
                        .ToList();
                }

                if (!config.UpdateMajorVersions)
                {
                    versions = versions
                        .Where(v => v.Major <= currentVersion.Major)
                        .ToList();
                }

                if (!versions.Any())
                {
                    return new List<IPackageInfo>();
                }

                var versionComparer = new VersionComparer();

                var sortedByVersion = versions
                    .OrderByDescending(c => c, versionComparer)
                    .ToList();

                var latestNeededVersion = sortedByVersion.FirstOrDefault();

                var versionComparison = versionComparer.Compare(latestNeededVersion, currentVersion);

                if (versionComparison == 1)
                {
                    result.Add(new PackageInfo { PackageId = packageId, Version = latestNeededVersion.OriginalVersion });
                }
            }

            return result;
        } 
    }

    public class Version
    {
        public string OriginalVersion { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public int Patch { get; set; }

        public string Prerelease { get; set; }

        public Version(string version)
        {
            OriginalVersion = version;
            
            var parts = version.Split('.');

            int.TryParse(parts[0], out int major);
            int.TryParse(parts[1], out int minor);

            Major = major;
            Minor = minor;

            if (parts[2].Contains('-'))
            {
                var patchString = parts[2].Split(separator: new char[] { '-' }, count: 2);

                int.TryParse(patchString[0], out int patch);

                Patch = patch;
                Prerelease = patchString[1];
            }
            else
            {
                int.TryParse(parts[2], out int patch);
                Patch = patch;
            }
        }
    }
}
