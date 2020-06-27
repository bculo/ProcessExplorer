using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses;
using ProcessExplorerWeb.Application.Processes.Queries.ProcessesSesionUserStats;
using ProcessExplorerWeb.Application.Processes.Queries.SearchProcessesPeriod;
using ProcessExplorerWeb.Application.Processes.Queries.SearchProcessesUser;
using ProcessExplorerWeb.Application.Processes.Queries.TopProcessesPeriod;
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
        public async Task<IActionResult> SearchProcesses([FromBody] SearchProcessesPeriodQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Search process
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("searchforuser")]
        public async Task<IActionResult> SearchUserProcesses([FromBody] SearchProcessesUserQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Msot used processes for some period
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("topprocessesperiod")]
        public async Task<IActionResult> GetTopProcessesForPeriod()
        {
            return Ok(await Mediator.Send(new TopProcessesPeriodQuery()));
        }

        /// <summary>
        /// Most used process by user
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("topprocessesuser")]
        public async Task<IActionResult> GetTopProcessesForUser()
        {
            return Ok(await Mediator.Send(new TopProcessesPeriodQuery()));
        }

        /// <summary>
        /// Get Processes Stats For Each Session For User
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("processstatsforsessions")]
        public async Task<IActionResult> GetProcessesStatsForEachSessionForUser()
        {
            return Ok(await Mediator.Send(new ProcessesSessionUserStatsQuery()));
        }

        /// <summary>
        /// Get day with most processes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("mostprocesses")]
        public async Task<IActionResult> GetDayWithMostProcesses()
        {
            return Ok(await Mediator.Send(new DayWithMostProcessesQuery()));
        }
    }
}
