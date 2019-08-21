using System.Collections.Generic;
using System.Xml.Linq;

namespace DependencyUpdaterCore.Models
{
    internal class CsProjPackageVersion : ICsProjPackageVersion
    {
        public XDocument File { get; set; }

        public IList<IPackageInfo> PackageInfos { get; set; }
    }

    internal class PackageInfo : IPackageInfo
    {
        public string PackageId { get; set; }

        public string Version { get; set; }
    }
}
