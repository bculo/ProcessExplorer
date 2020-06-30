using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcesses;
using ProcessExplorerWeb.Application.Processes.Queries.DayWithMostProcessesUser;
using ProcessExplorerWeb.Application.Processes.Queries.OSStatsPeriod;
using ProcessExplorerWeb.Application.Processes.Queries.OSStatsUser;
using ProcessExplorerWeb.Application.Processes.Queries.ProcessesSesionUserStats;
using ProcessExplorerWeb.Application.Processes.Queries.SearchProcessesPeriod;
using ProcessExplorerWeb.Application.Processes.Queries.SearchProcessesUser;
using ProcessExplorerWeb.Application.Processes.Queries.SearchSessionProcesses;
using ProcessExplorerWeb.Application.Processes.Queries.TopProcessesPeriod;
using ProcessExplorerWeb.Application.Processes.Queries.TopProcessesUser;
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
            return Ok(await Mediator.Send(new TopProcessesPeriodListQuery()));
        }

        /// <summary>
        /// Most used process by user
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("topprocessesuser")]
        public async Task<IActionResult> GetTopProcessesForUser()
        {
            return Ok(await Mediator.Send(new TopProcessesUserListQuery()));
        }

        /// <summary>
        /// Get Processes Stats For Each Session For User
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("processstatsforsessions")]
        public async Task<IActionResult> GetProcessesStatsForEachSessionForUser()
        {
            return Ok(await Mediator.Send(new UserSessionsStatsQuery()));
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

        /// <summary>
        /// Get day with most processes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("mostprocessesuser")]
        public async Task<IActionResult> GetDayWithMostProcessesUser()
        {
            return Ok(await Mediator.Send(new DayWithMostProcessesUserQuery()));
        }

        /// <summary>
        /// Get day with most processes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("osstatisticperiod")]
        public async Task<IActionResult> GetOsStatisticForPeriod()
        {
            return Ok(await Mediator.Send(new OSStatsPeriodQuery()));
        }

        /// <summary>
        /// Get day with most processes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("osstatisticperioduser")]
        public async Task<IActionResult> GetOsStatisticForPeriodUser()
        {
            return Ok(await Mediator.Send(new OSStatsUserQuery()));
        }

        /// <summary>
        /// Get day with most processes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("searchchoosensession")]
        public async Task<IActionResult> SearchSession([FromBody] SearchSessionProcessesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
