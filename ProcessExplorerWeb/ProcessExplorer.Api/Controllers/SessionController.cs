using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Session.Queries.GetSeletedSession;
using ProcessExplorerWeb.Application.Session.Queries.GetSessions;
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
    }
}
