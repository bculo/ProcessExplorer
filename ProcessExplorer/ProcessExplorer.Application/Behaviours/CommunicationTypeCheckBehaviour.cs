using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Behaviours;
using ProcessExplorer.Application.Common.Interfaces.Clients;
using ProcessExplorer.Application.Common.Interfaces.Services;
using ProcessExplorer.Core.Enums;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class CommunicationTypeCheckBehaviour : ICommunicationTypeBehaviour
    {
        protected readonly ICommunicationTypeClient _communication;
        protected readonly ICommunicationTypeService _communicationTypeService;
        protected readonly IInternet _internet;
        protected readonly ILoggerWrapper _logger;
        protected readonly IDateTime _time;
        protected readonly ISessionService _session;

        public CommunicationTypeCheckBehaviour(ICommunicationTypeClient communication,
            ICommunicationTypeService communicationTypeService,
            IInternet internet,
            ILoggerWrapper wrapper,
            IDateTime dateTime,
            ISessionService session)
        {
            _communication = communication;
            _communicationTypeService = communicationTypeService;
            _logger = wrapper;
            _time = dateTime;
            _internet = internet;
            _session = session;
        }

        public async Task Check()
        {
            _logger.LogInfo($"Started fetching communication type: {_time.Now}");

            if (!await _internet.CheckForInternetConnectionAsync() || _session.SessionInformation.Offline)
            {
                _logger.LogInfo($"Can connect to commuincation point: {_time.Now}");
                return;
            }

            CommunicationType type = await _communication.GetCommunicationType();

            if (type != _communicationTypeService.GetCommunicationType())
            {
                _communicationTypeService.SetCommuncationType(type);
            }

            _logger.LogInfo($"Finished fetching communication type: {_time.Now} | type is {type}");
        }
    }
}
