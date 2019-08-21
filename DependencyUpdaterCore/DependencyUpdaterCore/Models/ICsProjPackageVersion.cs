using System.Collections.Generic;
using System.Xml.Linq;

namespace DependencyUpdaterCore.Models
{
    public interface ICsProjPackageVersion
    {
        XDocument File { get; }
        IList<IPackageInfo> PackageInfos { get; }
    }

    public interface IPackageInfo
    {
        string PackageId { get; }
        string Version { get; }
    }
}
