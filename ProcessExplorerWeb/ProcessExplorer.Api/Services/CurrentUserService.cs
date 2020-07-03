using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Linq;
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

        private Guid id;

        public Guid UserId
        {
            get
            {
                if (id != Guid.Empty) //manually defiend id
                    return id;

                string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userId, out Guid userGuidFirst))
                    return userGuidFirst;
                return Guid.Empty;
            }
            set
            {
                id = value;
            }
        }
    }
}
