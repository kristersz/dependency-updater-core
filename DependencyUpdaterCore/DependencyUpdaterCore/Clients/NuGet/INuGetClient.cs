﻿using System.Threading.Tasks;

namespace DependencyUpdaterCore.Clients.NuGet
{
    public interface INuGetClient
    {
        Task<string> GetLatestPackageVersion(string packageId);
    }
}
