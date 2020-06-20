using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Interfaces;
using ProcessExplorer.Persistence;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer
{
    public class LoginBehaviour : IStartupPoint
    {
        private readonly ILoggerWrapper _logger;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationClient _client;
        private readonly IInternet _internet;
        private readonly ISessionService _session;

        private bool FreshToken { get; set; } = false;

        public LoginBehaviour(ILoggerWrapper logger,
            ITokenService tokenService,
            IAuthenticationClient client,
            IInternet internet,
            ISessionService session)
        {
            _logger = logger;
            _tokenService = tokenService;
            _client = client;
            _internet = internet;
            _session = session;
        }

        public async Task Start()
        {
            PromptMessage("Authenticaiton process started", true);

            if (!await _internet.CheckForInternetConnectionAsync())
            {
                PromptMessage("Internet access not available", true);
                throw new Exception("Internet acces not available");
            }

            if (!await _tokenService.TokenAvailable())
                await GetUserCredentials();

            if (!FreshToken) //using old token
            {
                _logger.LogInfo("Token available in storage");
                string oldJwtToken = await _tokenService.GetLastToken();
                var valid = await _client.ValidateToken(oldJwtToken);
                if (!valid) //invalid token, force user to enter username and password
                    await GetUserCredentials();
                else
                    _tokenService.SetValidToken(oldJwtToken);
            }

            if(!await _client.RegisterSession(_session.SessionInformation))
            {
                PromptMessage("Coludnt start session", true);
                throw new Exception("Coludnt start session");
            }

            PromptMessage("Authenticaiton process finished", true);
        }

        private async Task GetUserCredentials()
        {
            _logger.LogInfo("Forcing user to enter username and password");

            while (true)
            {
                PromptMessage("Enter username: ");
                string username = Console.ReadLine();
                PromptMessage("Enter password: ");
                string password = Console.ReadLine();

                var response = await _client.Login(username, password);
                if (response != null) //valid user
                {
                    _logger.LogInfo($"User{username} loged in");
                    await _tokenService.AddNewToken(response.JwtToken);
                    FreshToken = true;
                    break;
                }

                Console.Clear();
                PromptMessage("Wrong credentials", true);
            }
        }

        private void PromptMessage(string message, bool useNewLine = false)
        {
            if(!useNewLine)
                Console.Write(message);
            else
                Console.WriteLine(message);
        }
    }
}
 