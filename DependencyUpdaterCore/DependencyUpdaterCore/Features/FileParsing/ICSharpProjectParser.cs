using DependencyUpdaterCore.Models;
using System.Threading.Tasks;

namespace DependencyUpdaterCore.Features.FileParsing
{
    public interface ICSharpProjectParser
    {
        ICsProjPackageVersion GetCsProjDependencyInfo(byte[] file);
    }
}
