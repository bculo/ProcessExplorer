using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessExplorer.Api.Models.Authentication;
using ProcessExplorerWeb.Application.Common.Interfaces;

namespace ProcessExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _service.UserCredentialsValid(model.Identifier, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var dtoModel = _mapper.Map<LoginResponseModel>(await _service.GenerateJwtToken(model.Identifier));

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
    }
}
