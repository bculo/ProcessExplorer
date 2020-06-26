using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Processes.Queries.GetProcessesPeriod;
using ProcessExplorerWeb.Application.Processes.Queries.GetProcessesUser;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    [Authorize]
    public class ProcessController : MediatRBaseController
    {
        /// <summary>
        /// Search process
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("searchall")]
        public async Task<IActionResult> SearchProcesses([FromBody] GetProcessesPeriodQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Search process
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("searchforuser")]
        public async Task<IActionResult> SearchUserProcesses([FromBody] GetProcessesUserQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
