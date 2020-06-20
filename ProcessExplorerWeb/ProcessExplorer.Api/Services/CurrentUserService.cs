using Microsoft.AspNetCore.Http;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Security.Claims;

namespace ProcessExplorer.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        private ClaimsPrincipal User => _http.HttpContext?.User;

        public Guid UserId
        {
            get
            {
                string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userId, out Guid userGuid))
                    return userGuid;
                return Guid.Empty;
            }
        }
    }
}
