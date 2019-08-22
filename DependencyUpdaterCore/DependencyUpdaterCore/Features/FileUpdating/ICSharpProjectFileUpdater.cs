using DependencyUpdaterCore.Models;
using System.Collections.Generic;

namespace DependencyUpdaterCore.Features.FileUpdating
{
    public interface ICSharpProjectFileUpdater
    {
        IList<UpdatedCsProjFile> UpdateCsProjFile(ICsProjPackageVersion csProjectFile, IList<IPackageInfo> updates);
    }
}
