using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Entities;
using ProcessExplorer.Interfaces;
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
        private readonly IUnitOfWork _work;

        private bool FreshToken { get; set; } = false;

        public LoginBehaviour(ILoggerWrapper logger,
            ITokenService tokenService,
            IAuthenticationClient client,
            IInternet internet,
            ISessionService session,
            IUnitOfWork work)
        {
            _logger = logger;
            _tokenService = tokenService;
            _client = client;
            _internet = internet;
            _session = session;
            _work = work;
        }

        public async Task Start()
        {
            PromptMessage("Authenticaiton process started", true);

            if (!await _internet.CheckForInternetConnectionAsync())
            {
                PromptMessage("Internet access not available", true);
                PromptMessage("Starting work in offline mode", true);
                await RegisterSessionLocally();
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

            await SessionRegistrationWithServer();

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

        /// <summary>
        /// Register session first locally then on server
        /// This method should only be called if internet access is available
        /// Method throws exception if session is not registered on server
        /// </summary>
        /// <returns></returns>
        private async Task SessionRegistrationWithServer()
        {
            await RegisterSessionLocally();

            //Server session registration
            if (!await _client.RegisterSession(_session.SessionInformation))
                throw new Exception("Coludnt start session");
        }

        /// <summary>
        /// Register ssesion only localy
        /// </summary>
        /// <returns></returns>
        private async Task RegisterSessionLocally()
        {
            //Check if there is an session in database with same statring time
            var savedSession = await _work.Sessions.GetCurrentSession(_session.SessionInformation.SessionStarted);

            if (savedSession != null) //session with same start time is already in database
                _session.ChangeSessionId(savedSession.Id);
            else
            {
                //store new session localy
                var sessionEntity = _session.SessionInformation.Adapt<Session>();
                _work.Sessions.Add(sessionEntity);
                await _work.CommitAsync();
            }
        }

        /// <summary>
        /// Force user to type identifier and password
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Prompt message to console
        /// </summary>
        /// <param name="message"></param>
        /// <param name="useNewLine"></param>
        private void PromptMessage(string message, bool useNewLine = false)
        {
            if(!useNewLine)
                Console.Write(message);
            else
                Console.WriteLine(message);
        }
    }
}
 