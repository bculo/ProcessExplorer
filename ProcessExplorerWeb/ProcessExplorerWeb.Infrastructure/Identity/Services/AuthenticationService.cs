using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Models.Security;
using ProcessExplorerWeb.Application.Common.Models.Service;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Interfaces;
using ProcessExplorerWeb.Infrastructure.Identity.Extensions;
using ProcessExplorerWeb.Infrastructure.Interfaces;
using ProcessExplorerWeb.Infrastructure.Options;
using ProcessExplorerWeb.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Identity.Services
{
    /// <summary>
    /// IAuthenticationService interface implementation
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DbContext _context; //transaction purpose only

        private readonly UserManager<IdentityAppUser> _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly AuthenticationOptions _authOptions;
        private readonly ILogger<AuthenticationService> _log;
        private readonly RoleManager<IdentityAppRole> _roleManager;
        private readonly IUnitOfWork _work;

        public AuthenticationService(ProcessExplorerDbContext context,
            UserManager<IdentityAppUser> userManager,
             ITokenManager tokenManager,
             IOptions<AuthenticationOptions> authOptions,
             ILogger<AuthenticationService> log,
             RoleManager<IdentityAppRole> roleManager,
             IUnitOfWork work)
        {
            _context = context;
            _userManager = userManager;
            _tokenManager = tokenManager;
            _authOptions = authOptions.Value;
            _log = log;
            _roleManager = roleManager;
            _work = work;
        }

        #region HELPER METHODS

        /// <summary>
        /// Create claim for selected user
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="roleName">role we want to add to claims</param>
        /// <returns>Array of Claims</returns>
        private Claim[] CreateClaims(IdentityAppUser user, string roleName)
        {
            _log.LogInformation("Generating claims for user {@user}", user);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName),
            };

            return claims;
        }

        /// <summary>
        /// Get default application role
        /// Throws exception if there are no default role
        /// </summary>
        /// <returns>Default application role</returns>
        private async Task<IdentityAppRole> GetDefaultRole()
        {
            _log.LogInformation("Fetching default role");

            var roleInstance = await _roleManager.FindByNameAsync(_authOptions.DefaultRole);

            if (roleInstance == null)
            {
                var exception = new Exception("DEFAULT ROLE NOT FOUND");
                _log.LogError(exception.Message, exception);
                throw exception;
            }

            return roleInstance;
        }

        #endregion

        #region INTERFACE IMPLEMENTATION

        /// <summary>
        /// Generate JWT token using user information
        /// </summary>
        /// <param name="identifier">Id | email | password</param>
        /// <returns>Instance of TokenInfo</returns>
        public async Task<TokenInfo> GenerateJwtToken(string identifier)
        {
            var user = await GetUserByIdentifier(identifier) as IdentityAppUser;

            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);

            _log.LogInformation("Generating JWT token for user {@user}", user);

            var (jwtToken, expiration) = await _tokenManager.GenerateToken(userClaims);

            return new TokenInfo(user, jwtToken, expiration);
        }

        /// <summary>
        /// Get user by Id or email or username
        /// </summary>
        /// <param name="identifier">Id or email or password</param>
        /// <returns>Instance of IProcessExplorerUser</returns>
        public async Task<IProcessExplorerUser> GetUserByIdentifier(string identifier)
        {
            _log.LogInformation($"Fetching user by identifier {identifier}");

            if (Guid.TryParse(identifier, out Guid result))
            {
                return await _userManager.FindByIdAsync(result.ToString());
            }
            else if (identifier.Contains("@"))
            {
                return await _userManager.FindByEmailAsync(identifier);
            }
            else
            {
                return await _userManager.FindByNameAsync(identifier);
            }
        }

        /// <summary>
        /// Normal user registration procedure
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <param name="profileImage">profileImagePath</param>
        /// <returns>Instance of IResult and IUser</returns>
        public async Task<(Result, IProcessExplorerUser)> RegisterUser(string username, string email, string password, string profileImage)
        {
            //Value cant be null
            var userRole = await GetDefaultRole();

            _log.LogInformation($"User registration started");

            //start code transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try //transaction code
            {
                //create IdentityAppUser instance and add default IdentityAppUserRole role to him
                var newUser = new IdentityAppUser { Email = email, UserName = username };
                newUser.UserRoles.Add(new IdentityAppUserRole { Role = userRole });

                //Try to add IdentityAppUser to database
                var identityResultUser = await _userManager.CreateAsync(newUser, password);

                //Check status of previous operation 
                if (!identityResultUser.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return (identityResultUser.GetApplicationResult(), null);
                }

                //create claims add add claims to created user
                var claims = CreateClaims(newUser, userRole.Name);
                var identityResultClaim = await _userManager.AddClaimsAsync(newUser, claims);

                //Check status of previous operation
                if (!identityResultClaim.Succeeded)
                {
                    //failed to create claims for user
                    await transaction.RollbackAsync();
                    return (identityResultClaim.GetApplicationResult(), null);
                }

                //Create new ProcessExplorerUser instance and add it to database
                var user = new ProcessExplorerUser(newUser.Id, newUser.Email, newUser.UserName);
                _work.User.Add(user);
                await _work.CommitAsync();

                //commit transaction
                await transaction.CommitAsync();
                _log.LogInformation("User registration finished {@user}", user);
                return (Result.Success(), user);
            }
            catch(Exception e) // if transaction fails
            {
                await transaction.RollbackAsync();
                _log.LogInformation("User registration failed");
                return (Result.Failure("Ann error occurred"), null);
            }
        }

        /// <summary>
        /// Check user credentials
        /// </summary>
        /// <param name="identifier">User identifier (Id or email or username)</param>
        /// <param name="password">user password</param>
        /// <returns>Instance of Result</returns>
        public async Task<Result> UserCredentialsValid(string identifier, string password)
        {
            //Find user by identifier
            var user = await GetUserByIdentifier(identifier);
            if (user == null)
                return Result.Failure("User not found");

            bool validPassword = await _userManager.CheckPasswordAsync(user as IdentityAppUser, password);
            return validPassword ? Result.Success() : Result.Failure("Invalid credentials");
        }

        #endregion
    }
}
