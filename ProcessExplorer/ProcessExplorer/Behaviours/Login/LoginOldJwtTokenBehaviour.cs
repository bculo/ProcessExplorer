using Dtos.Responses.Authentication;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Behaviours.Login
{
    public class LoginOldJwtTokenBehaviour : LoginBehaviourRoot
    {
        public LoginOldJwtTokenBehaviour(ILoggerWrapper logger,
            ITokenService tokenService, 
            IAuthenticationClient client, 
            IInternet internet, 
            ISessionService session, 
            IUnitOfWork work) 
            : base(logger, tokenService, client, internet, session, work)
        {
        }

        private bool FreshToken { get; set; } = false;

        public override async Task HandleLoginResult(LoginResponseDto responseDto)
        {
            if (responseDto != null) //valid user
            {
                _logger.LogInfo($"User with ID {responseDto.UserId} loged in");
                await _tokenService.AddNewToken(responseDto.JwtToken);
                FreshToken = true;

                //Exit validation
                Validating = false;
            }
            else
            {
                PromptMessage("Wrong credentials, please try again", true);
            }
        }

        public override async Task Start()
        {
            PromptMessage("Authenticaiton process started", true);

            await RegisterSessionLocally();

            bool internetAvailable = await _internet.CheckForInternetConnectionAsync();

            if (!internetAvailable)
            {
                PromptMessage("Internet access not available", true);
                PromptMessage("Starting work in offline mode", true);
                PromptMessage("Authenticaiton process finished", true);
                return;
            }

            // token not available in database
            if (!await _tokenService.TokenAvailable())
                await GetUserCredentials();

            if (!FreshToken) //using old token
            {
                var (valid, token) = await CheckOldToken();
                if (!valid) //invalid token, force user to enter username and password
                    await GetUserCredentials();
                else //set token for authentication
                    _tokenService.SetValidToken(token);
            }

            PromptMessage("Authenticaiton process finished", true);
        }

        /// <summary>
        /// Check if old token is valid
        /// </summary>
        /// <returns>(valid, jwttoken)</returns>
        private async Task<(bool, string)> CheckOldToken()
        {
            _logger.LogInfo("Token available in storage");
            string oldJwtToken = await _tokenService.GetLastToken();
            return (await _client.ValidateToken(oldJwtToken), oldJwtToken);
        }
    }
}
