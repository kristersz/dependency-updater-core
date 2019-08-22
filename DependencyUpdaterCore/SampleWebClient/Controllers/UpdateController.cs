using DependencyUpdaterCore;
using DependencyUpdaterCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SampleWebClient.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IDependencyUpdater _dependencyUpdater;

        public UpdateController(IDependencyUpdater dependencyUpdater)
        {
            _dependencyUpdater = dependencyUpdater;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Trigger([FromBody]UpdateBody model)
        {
            try
            {
                await _dependencyUpdater.UpdateDependencies(new UpdateCheckingConfig
                {
                    IncludePrereleaseVersions = model.TakePreviews,
                    UpdateMajorVersions = model.TakeMajor
                });
                return Ok();
            }
            catch (Exception)
            {
                return Ok();
            }
        }
    }

    public class UpdateBody
    {
        public bool TakePreviews { get; set; }
        public bool TakeMajor { get; set; }
        public string Repo { get; set; }
    }
}
