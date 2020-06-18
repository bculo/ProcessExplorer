using ProcessExplorerWeb.Application.Common.Models.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Interfaces
{
    public interface ITokenManager
    {
        /// <summary>
        /// Generate JWT token
        /// </summary>
        /// <param name="claims">user claims</param>
        /// <returns>jwt token and token expiration time</returns>
        Task<(string jwtToken, long expireIn)> GenerateToken(IEnumerable<Claim> claims);
    }
}
