using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Processes.Queries.GetProcessesPeriod;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
