using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessExplorerWeb.Application.Sync.Commands;
using ProcessExplorerWeb.Application.Sync.Commands.SyncApplication;
using ProcessExplorerWeb.Application.Sync.Commands.SyncSession;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    [Authorize]
    public class SynchronizationController : MediatRBaseController
    {

        /// <summary>
        /// End point for synchornization
        /// </summary>
        /// <returns></returns>
        [HttpPost("sync")]
        public async Task<IActionResult> Sync([FromBody] SyncSessionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// End point for synchornization
        /// </summary>
        /// <returns></returns>
        [HttpPost("sync/applications")]
        public async Task<IActionResult> SyncApps([FromBody] SyncApplicationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
