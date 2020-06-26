using Dtos.Responses.Authentication;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IAuthenticationClient
    {
        /// <summary>
        /// Validate old token
        /// </summary>
        /// <param name="jwtToken">JWT token</param>
        /// <returns></returns>
        public Task<bool> ValidateToken(string jwtToken);

        /// <summary>
        /// Login using username and password
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public Task<LoginResponseDto> Login(string identifier, string password);
    }
}
