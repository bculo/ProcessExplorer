using MediatR;

namespace ProcessExplorerWeb.Application.Authentication.Queries.CheckToken
{
    public class CheckTokenQuery : IRequest
    {
        public string Token { get; set; }
    }
}
