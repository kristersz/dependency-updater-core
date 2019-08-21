using DependencyUpdaterCore.Features.FileUpdating;
using DependencyUpdaterCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DependencyUpdaterCore.Tests.Features.FileUpdating
{
    [TestClass]
    public class CSharpProjectFileUpdaterTest
    {
        [TestMethod]
        public void UpdateCsProjFile()
        {
            var updater = new CSharpProjectFileUpdater();

            var csProjFile = @"
<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include=""Features\FileFetching\"" />
    <Folder Include=""Features\FileParsing\"" />
    <Folder Include=""Features\FileUpdating\"" />
    <Folder Include=""Features\PullRequestCreation\"" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include=""Microsoft.TeamFoundationServer.Client"" Version=""16.143.2"" />
    <PackageReference Include=""Microsoft.VisualStudio.Services.InteractiveClient"" Version=""16.143.2"" />
    <PackageReference Include=""Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi"" Version=""16.143.2"" />
    <PackageReference Include=""Newtonsoft.Json"" Version=""12.0.2"" />
  </ItemGroup>

</Project>
";

            var file = new CsProjPackageVersion
            {
                File = XDocument.Parse(csProjFile)
            };

            var latestVersions = new List<IPackageInfo>
            {
                new PackageInfo { PackageId = "Newtonsoft.Json", Version = "12.0.3" },
                new PackageInfo { PackageId = "Microsoft.TeamFoundationServer.Client", Version = "16.143.1" }
            };

            var result = updater.UpdateCsProjFile(file, latestVersions);

            Assert.IsNotNull(result);
        }
    }
}
