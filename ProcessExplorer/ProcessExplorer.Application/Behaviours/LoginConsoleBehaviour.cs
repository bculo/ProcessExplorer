using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class LoginConsoleBehaviour : ILoginBehaviour
    {
        private readonly ILoggerWrapper _logger;
        private readonly IDateTime _time;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationClient _client;

        private bool FreshToken { get; set; } = false;

        public LoginConsoleBehaviour(ILoggerWrapper logger,
            IDateTime time,
            ITokenService tokenService,
            IAuthenticationClient client)
        {
            _logger = logger;
            _time = time;
            _tokenService = tokenService;
            _client = client;
        }

        public async Task ValidateUser()
        {
            if (!await _tokenService.TokenAvailable())
                await GetUserCredentials();

            if (!FreshToken) //using old token
            {
                _logger.LogInfo("Token available in storage");
                string oldJwtToken = await _tokenService.GetToken();
                bool valid = await _client.ValidateToken(oldJwtToken);
                if (!valid) //invalid token, force user to enter username and password
                    await GetUserCredentials();
            }
        }

        private async Task GetUserCredentials()
        {
            _logger.LogInfo("Forcing user to enter username and password");

            while(true)
            {
                PromptMessage("Enter username: ");
                string username = Console.ReadLine();
                PromptMessage("Enter password: ");
                string password = Console.ReadLine();

                var response = await _client.Login(username, password);
                if(!string.IsNullOrEmpty(response)) //valid user
                {
                    _logger.LogInfo($"User{username} loged in");
                    await _tokenService.SetNewToken(response);
                    FreshToken = true;
                    break;
                }

                Console.Clear();
            }
        }

        private void PromptMessage(string message)
        {
            Console.Write(message);
        }
    }
}
