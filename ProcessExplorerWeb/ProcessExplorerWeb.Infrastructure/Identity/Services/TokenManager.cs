using ProcessExplorerWeb.Core.Interfaces;
using ProcessExplorerWeb.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Identity.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IDateTime _time;

        public TokenManager()
        {

        }

        public Task<(string jwtToken, long expireIn)> GenerateToken(IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }
    }
}
