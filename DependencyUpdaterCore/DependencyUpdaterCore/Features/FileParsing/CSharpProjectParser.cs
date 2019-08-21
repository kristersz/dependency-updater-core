using DependencyUpdaterCore.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

[assembly: InternalsVisibleTo("DependencyUpdaterCore.Tests")]
namespace DependencyUpdaterCore.Features.FileParsing
{
    internal class CSharpProjectParser : ICSharpProjectParser
    {
        public ICsProjPackageVersion GetCsProjDependencyInfo(byte[] file)
        {
            try
            {
                var fileString = Encoding.Default.GetString(file);

                var xDocument = XDocument.Parse(fileString);

                var packageReferences = xDocument
                    .XPathSelectElements("Project/ItemGroup/PackageReference");

                var packageInfos = new List<IPackageInfo>();

                foreach (var item in packageReferences)
                {
                    packageInfos.Add(new PackageInfo
                    {
                        PackageId = item.Attribute("Include").Value,
                        Version = item.Attribute("Version").Value
                    });
                }

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
