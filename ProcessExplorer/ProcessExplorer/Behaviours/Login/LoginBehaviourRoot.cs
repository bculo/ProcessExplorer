using Dtos.Responses.Authentication;
using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Clients;
using ProcessExplorer.Application.Common.Interfaces.Services;
using ProcessExplorer.Core.Entities;
using ProcessExplorer.Core.Enums;
using ProcessExplorer.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Behaviours.Login
{
    /// <summary>
    /// Abstract class for Authentication
    /// </summary>
    public abstract class LoginBehaviourRoot : IStartupPoint
    {
        protected readonly ILoggerWrapper _logger;
        protected readonly ITokenService _tokenService;
        protected readonly IAuthenticationClient _client;
        protected readonly IInternet _internet;
        protected readonly ISessionService _session;
        protected readonly IUnitOfWork _work;
        protected readonly ICommunicationTypeClient _communication;
        protected readonly ICommunicationTypeService _communicationTypeService;

        public abstract Task Start();
        public abstract Task HandleLoginResult(LoginResponseDto responseDto);

        protected bool Validating { get; set; } = true;

        protected LoginBehaviourRoot(ILoggerWrapper logger,
            ITokenService tokenService,
            IAuthenticationClient client,
            IInternet internet,
            ISessionService session,
            IUnitOfWork work,
            ICommunicationTypeClient communication,
            ICommunicationTypeService communicationTypeService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _client = client;
            _internet = internet;
            _session = session;
            _work = work;
            _communication = communication;
            _communicationTypeService = communicationTypeService;
        }

        /// <summary>
        /// Prompt message to console
        /// </summary>
        /// <param name="message"></param>
        /// <param name="useNewLine"></param>
        protected void PromptMessage(string message, bool useNewLine = false)
        {
            if (!useNewLine)
                Console.Write(message);
            else
                Console.WriteLine(message);
        }


        /// <summary>
        /// Register ssesion only localy
        /// </summary>
        /// <returns></returns>
        protected virtual async Task RegisterSessionLocally()
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
        /// Read user input
        /// </summary>
        /// <returns></returns>
        protected string ReadInput()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Force user to type identifier and password
        /// </summary>
        /// <returns></returns>
        protected async Task GetUserCredentials()
        {
            _logger.LogInfo("Forcing user to enter username and password");

            while (Validating)
            {
                PromptMessage("Enter username: ");
                string username = ReadInput();
                PromptMessage("Enter password: ");
                string password = ReadInput();

                try
                {
                    var response = await _client.Login(username, password);
                    await HandleLoginResult(response);
                }
                catch
                {
                    //Error are thrown only if service not available or timeout happend
                    PromptMessage("Service not available", true);
                    PromptMessage("Starting work in offline mode", true);
                    _session.SetMode(WorkMode.OFFLINE);
                    Validating = false;
                }
            }
        }
    }
}
