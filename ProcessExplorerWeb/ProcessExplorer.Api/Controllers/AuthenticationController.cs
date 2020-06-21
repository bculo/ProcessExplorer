using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorer.Api.Models.Authentication;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _service.UserCredentialsValid(model.Identifier, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var tokenInfo = await _service.GenerateJwtToken(model.Identifier);

            var dtoModel = new LoginResponseModel
            {
                JwtToken = tokenInfo.JwtToken,
                UserId = tokenInfo.User.Id
            };

            return Ok(dtoModel);
        }

        [HttpPost("checktoken")]
        public async Task<IActionResult> CheckToken([FromBody] CheckTokenModel model)
        {
            var valid = await _service.IsTokenValid(model.Token);

            if (!valid)
                return BadRequest();

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var (result, user) = await _service.RegisterUser(model.UserName, model.Email, model.Password, null);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [Authorize]
        [HttpPost("sessionregistration")]
        public async Task<IActionResult> RegisterSession([FromBody] SessionModel model)
        {
            await _service.RegisterSession(model.UserName, model.SesssionId, model.Started);
            return Ok();
        }
    }
}
