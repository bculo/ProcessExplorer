using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Modules.Application.Queries.AppPerSessionStatistic;
using ProcessExplorerWeb.Application.Modules.Application.Queries.DayWithMostApps;
using ProcessExplorerWeb.Application.Modules.Application.Queries.DayWithMostAppsUser;
using ProcessExplorerWeb.Application.Modules.Application.Queries.OSStatsPeriod;
using ProcessExplorerWeb.Application.Modules.Application.Queries.OSStatsUser;
using ProcessExplorerWeb.Application.Modules.Application.Queries.SearchAppsPeriod;
using ProcessExplorerWeb.Application.Modules.Application.Queries.SearchAppsUser;
using ProcessExplorerWeb.Application.Modules.Application.Queries.SearchSessionsApps;
using ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppListPeriod;
using ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppListUser;
using ProcessExplorerWeb.Application.Modules.Application.Queries.TopOpenedAppSession;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    [Authorize]
    public class ApplicationController : MediatRBaseController
    {

        [HttpGet("daywithmostappsperiod")]
        public async Task<IActionResult> GetDayWithMostAppsForPeriod()
        {
            return Ok(await Mediator.Send(new DayWithMostAppsQuery { }));
        }

        [HttpGet("daywithmostappsuser")]
        public async Task<IActionResult> GetDayWithMostAppsForUser()
        {
            return Ok(await Mediator.Send(new DayWithMostAppsUserQuery { }));
        }

        [HttpGet("osappstatisticperiod")]
        public async Task<IActionResult> GetOSApplicationStatisticsForPeriod()
        {
            return Ok(await Mediator.Send(new OSStatsPeriodQuery { }));
        }

        [HttpGet("osappstatisticuser")]
        public async Task<IActionResult> GetOSApplicationStatisticsForUser()
        {
            return Ok(await Mediator.Send(new OSStatsUserQuery { }));
        }

        [HttpPost("searchappsperiod")]
        public async Task<IActionResult> SearchAppplicaitonPeriod([FromBody] SearchAppsPeriodQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("searchappsuser")]
        public async Task<IActionResult> SearchAppplicaitonUser([FromBody] SearchAppsUserQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("searchappsinsession")]
        public async Task<IActionResult> SearchApplicationsInSession([FromBody] SearchSessionsAppsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("topopenedappsperiod")]
        public async Task<IActionResult> GetMostOpenedAppsForPeriod()
        {
            return Ok(await Mediator.Send(new TopOpenedAppListPeriodQuery { } ));
        }

        [HttpGet("topopenedappsuser")]
        public async Task<IActionResult> GetMostOpenedAppsForUser()
        {
            return Ok(await Mediator.Send(new TopOpenedAppListUserQuery { }));
        }

        [HttpPost("topopenedappssession")]
        public async Task<IActionResult> GetMostOpenedAppsForSession([FromBody] TopOpenedAppSessionQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("openedappspersession")]
        public async Task<IActionResult> GetStatisticForSessions()
        {
            return Ok(await Mediator.Send(new AppPerSessionStatisticQuery { } ));
        }
    }
}
