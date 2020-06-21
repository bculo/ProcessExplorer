using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Sync.Commands;
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
    }
}
