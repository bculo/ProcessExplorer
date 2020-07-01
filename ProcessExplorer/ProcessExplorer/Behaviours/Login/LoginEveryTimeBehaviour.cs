using Dtos.Responses.Authentication;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Clients;
using ProcessExplorer.Application.Common.Interfaces.Services;
using ProcessExplorer.Core.Enums;
using System.Threading.Tasks;

namespace ProcessExplorer.Behaviours.Login
{
    public class LoginEveryTimeBehaviour : LoginBehaviourRoot
    {
        public LoginEveryTimeBehaviour(ILoggerWrapper logger,
            ITokenService tokenService,
            IAuthenticationClient client,
            IInternet internet,
            ISessionService session,
            IUnitOfWork work,
            ICommunicationTypeClient communication,
            ICommunicationTypeService communicationTypeService)
            : base(logger, tokenService, client, internet, session, work, communication, communicationTypeService)
        {
        }

        public override async Task HandleLoginResult(LoginResponseDto responseDto)
        {
            if (responseDto != null) //valid user
            {
                _logger.LogInfo($"User with ID {responseDto.UserId} loged in");
                await _tokenService.AddNewToken(responseDto.JwtToken);

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
                _session.SetMode(WorkMode.OFFLINE);
                PromptMessage("Internet access not available", true);
                PromptMessage("Starting work in offline mode", true);
            }
            else
            {
                await GetUserCredentials(); // Force user to enter valid credentials
            }

            //get communication type for server
            if (!_session.SessionInformation.Offline)
            {
                PromptMessage("Establishing communication type", true);
                CommunicationType type = await _communication.GetCommunicationType();
                _communicationTypeService.SetCommuncationType(type);
                PromptMessage($"Communication type {type}", true);
            }
            else
            {
                PromptMessage($"Communication type {_communicationTypeService.GetCommunicationType()}", true);
            }

            PromptMessage("Authenticaiton process finished", true);
        }
    }
}
 