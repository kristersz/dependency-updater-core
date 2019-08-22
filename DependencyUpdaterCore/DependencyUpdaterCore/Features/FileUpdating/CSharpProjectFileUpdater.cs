using DependencyUpdaterCore.Features.UpdateChecking;
using DependencyUpdaterCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DependencyUpdaterCore.Features.FileUpdating
{
    public class CSharpProjectFileUpdater : ICSharpProjectFileUpdater
    {
        public IList<UpdatedCsProjFile> UpdateCsProjFile(ICsProjPackageVersion csProjectFile, IList<IPackageInfo> packageInfos)
        {
            var resultFiles = new List<UpdatedCsProjFile>();

            var packageReferences = csProjectFile.File
                    .XPathSelectElements("Project/ItemGroup/PackageReference");

            foreach (var item in packageReferences)
            {
                var currentVersion = item?.Attribute("Version")?.Value;
                var packageId = item?.Attribute("Include")?.Value;

                if (string.IsNullOrEmpty(currentVersion) ||
                    string.IsNullOrEmpty(packageId))
                {
                    continue;
                }

                var packageInfo = packageInfos.FirstOrDefault(v => v.PackageId == packageId);

                if (ShouldUpdatePackageVersion(packageInfo, currentVersion))
                {
                    var clone = new XDocument(csProjectFile.File);

                    var packageReference = clone.XPathSelectElements($"Project/ItemGroup/PackageReference[@Include='{packageId}']").FirstOrDefault();

                    packageReference.Attribute("Version").SetValue(packageInfo.Version);

                    resultFiles.Add(new UpdatedCsProjFile { FileContent = clone.ToString(), PackageId = packageId });
                }  
            }

            return resultFiles;
        }

        private bool ShouldUpdatePackageVersion(IPackageInfo packageInfo, string currentVersion)
        {
            if (packageInfo == null || string.IsNullOrWhiteSpace(packageInfo.Version))
            {
                return false;
            }

            var versionComparison = new VersionComparer().Compare(packageInfo.Version, currentVersion);

            if (versionComparison == 1)
            {
                return true;
            }

            return false;
        }
    }
}
