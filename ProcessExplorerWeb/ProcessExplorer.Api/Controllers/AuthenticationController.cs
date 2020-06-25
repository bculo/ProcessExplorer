using Microsoft.AspNetCore.Mvc;
using ProcessExplorerWeb.Application.Authentication.Commands.Register;
using ProcessExplorerWeb.Application.Authentication.Queries.CheckToken;
using ProcessExplorerWeb.Application.Authentication.Queries.Login;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    public class AuthenticationController : MediatRBaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("checktoken")]
        public async Task<IActionResult> CheckToken([FromBody] CheckTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
