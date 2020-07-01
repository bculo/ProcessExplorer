using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Modules.Communication.Commands.ChangeType;
using ProcessExplorerWeb.Application.Modules.Communication.Queries.GetType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    [Authorize]
    public class CommunicationController : MediatRBaseController
    {
        [HttpPost]
        public async Task<IActionResult> SetCommunicationType([FromBody] ChangeTypeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCommunicationType()
        {
            return Ok(await Mediator.Send(new GetTypeQuery { }));
        }
    }
}
