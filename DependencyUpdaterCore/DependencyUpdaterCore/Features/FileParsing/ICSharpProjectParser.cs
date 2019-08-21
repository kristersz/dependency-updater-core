using DependencyUpdaterCore.Models;
using System.IO;

namespace DependencyUpdaterCore.Features.FileParsing
{
    public interface ICSharpProjectParser
    {
        ICsProjPackageVersion GetCsProjDependencyInfo(Stream fileStream);
    }
}
