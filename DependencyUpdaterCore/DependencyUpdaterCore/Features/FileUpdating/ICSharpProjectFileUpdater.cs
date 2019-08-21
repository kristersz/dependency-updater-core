using DependencyUpdaterCore.Models;
using System.Collections.Generic;

namespace DependencyUpdaterCore.Features.FileUpdating
{
    public interface ICSharpProjectFileUpdater
    {
        IList<string> UpdateCsProjFile(ICsProjPackageVersion csProjectFile, IList<IPackageInfo> packageInfos);
    }
}
