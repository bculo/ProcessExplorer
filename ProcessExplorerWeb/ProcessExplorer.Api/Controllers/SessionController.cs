using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Session.Queries.GetSeletedSession;
using ProcessExplorerWeb.Application.Session.Queries.GetSessions;
using ProcessExplorerWeb.Application.Session.Queries.SessionStatisticsPeriod;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    public class SessionController : MediatRBaseController
    {
        [HttpPost("listsessions")]
        public async Task<IActionResult> GetUserSessions([FromBody] GetSessionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("getsession")]
        public async Task<IActionResult> GetChoosenSesion([FromBody] GetSelectedSessionQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("sessionstatistic")]
        public async Task<IActionResult> SessionsStatistic()
        {
            return Ok(await Mediator.Send(new SessionStatisticsAllQuery()));
        }

        [HttpGet("usersessionstatistic")]
        public async Task<IActionResult> UserSessionsStatistic()
        {
            return Ok(await Mediator.Send(new SessionStatisticsAllQuery()));
        }
    }
}
