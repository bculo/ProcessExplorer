using ProcessExplorerWeb.Application.Common.Models.Security;
using ProcessExplorerWeb.Application.Common.Models.Service;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    /// <summary>
    /// Authentication interface
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Check user credentials
        /// </summary>
        /// <param name="identifier">User identifier (Id or email or username)</param>
        /// <param name="password">user password</param>
        /// <returns>Instance of IResult</returns>
        Task<Result> UserCredentialsValid(string identifier, string password);

        /// <summary>
        /// Get user by Id or email or username
        /// </summary>
        /// <param name="identifier">Id or email or password</param>
        /// <returns>Instance of IProcessExplorerUser</returns>
        Task<IProcessExplorerUser> GetUserByIdentifier(string identifier);

        /// <summary>
        /// Generate JWT token using user information
        /// </summary>
        /// <param name="identifier">Id or email or password</param>
        /// <returns>Instnace of TokenInfo</returns>
        Task<TokenInfo> GenerateJwtToken(string identifier);

        /// <summary>
        /// Normal user registration
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>Instance of IResult and IProcessExplorerUser</returns>
        Task<(Result, IProcessExplorerUser)> RegisterUser(string username, string email, string password, string profileImage);
    }
}
