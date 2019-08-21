using DependencyUpdaterCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

[assembly: InternalsVisibleTo("DependencyUpdaterCore.Tests")]
namespace DependencyUpdaterCore.Features.FileParsing
{
    internal class CSharpProjectParser : ICSharpProjectParser
    {
        public ICsProjPackageVersion GetCsProjDependencyInfo(Stream fileStream)
        {
            try
            {
                var xDocument = XDocument.Load(fileStream);

                var packageReferences = xDocument
                    .XPathSelectElements("Project/ItemGroup/PackageReference");

                var packageInfos = new List<IPackageInfo>();

                foreach (var item in packageReferences)
                {
                    var version = item?.Attribute("Version")?.Value;
                    var id = item?.Attribute("Include")?.Value;

                    if (string.IsNullOrEmpty(version) ||
                        string.IsNullOrEmpty(id))
                    {
                        continue;
                    }

                    packageInfos.Add(new PackageInfo
                    {
                        PackageId = id,
                        Version = version
                    });
                }

                fileStream.Dispose();

                return new CsProjPackageVersion
                {
                    File = xDocument,
                    PackageInfos = packageInfos
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
