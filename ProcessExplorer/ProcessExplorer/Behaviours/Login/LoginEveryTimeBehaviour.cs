using Dtos.Responses.Authentication;
using ProcessExplorer.Application.Common.Interfaces;
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
            IUnitOfWork work)
            : base(logger, tokenService, client, internet, session, work)
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
                PromptMessage("Internet access not available", true);
                PromptMessage("Starting work in offline mode", true);
            }
            else
            {
                await GetUserCredentials();
            }

            PromptMessage("Authenticaiton process finished", true);
        }
    }
}
 