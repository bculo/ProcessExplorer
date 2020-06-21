using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ProcessExplorer.Api.Controllers
{
    /// <summary>
    /// MediatR base controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MediatRBaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
