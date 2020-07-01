using System.Collections.Generic;
using System.Security.Claims;

namespace ProcessExplorerWeb.Infrastructure.Interfaces
{
    public interface ITokenManager
    {
        /// <summary>
        /// Generate JWT token
        /// </summary>
        /// <param name="claims">user claims</param>
        /// <returns>jwt token and token expiration time</returns>
        (string jwtToken, long expireIn) GenerateToken(IEnumerable<Claim> claims);

        /// <summary>
        /// Check if token valid
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        bool TokenValid(string jwtToken);
    }
}
