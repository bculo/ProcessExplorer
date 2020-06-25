using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Session.Queries.GetSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    public class SessionController : MediatRBaseController
    {
        public async Task<IActionResult> GetUserSessions([FromBody] GetSessionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
