using System;

namespace ProcessExplorerWeb.Application.Authentication.Queries.Login
{
    public class LoginQueryResponseDto
    {
        public Guid UserId { get; set; }
        public string JwtToken { get; set; }
        public string UserName { get; set; }
        public long ExpireIn { get; set; }
    }
}
