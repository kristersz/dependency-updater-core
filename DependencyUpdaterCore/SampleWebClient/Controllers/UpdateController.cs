using DependencyUpdaterCore;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> Trigger()
        {
            try
            {
                await _dependencyUpdater.UpdateDependencies();
                return Ok();
            }
            catch (Exception)
            {
                return Ok();
            }
        }
    }
}
