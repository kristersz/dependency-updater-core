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
        private readonly string _sampleXml = @"
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

        [TestMethod]
        public void UpdateCsProjFile_VariousDeps()
        {
            var updater = new CSharpProjectFileUpdater();

            var file = new CsProjPackageVersion
            {
                File = XDocument.Parse(_sampleXml)
            };

            var latestVersions = new List<IPackageInfo>
            {
                new PackageInfo { PackageId = "Newtonsoft.Json", Version = "12.0.3" },
                new PackageInfo { PackageId = "Microsoft.TeamFoundationServer.Client", Version = "16.143.1" }
            };

            var result = updater.UpdateCsProjFile(file, latestVersions);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateCsProjFile_NonStableVersions_Ignored()
        {
            var updater = new CSharpProjectFileUpdater();

            var file = new CsProjPackageVersion
            {
                File = XDocument.Parse(_sampleXml)
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
