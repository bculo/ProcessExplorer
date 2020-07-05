using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Behaviours;
using ProcessExplorer.Application.Common.Interfaces.Clients;
using ProcessExplorer.Application.Common.Interfaces.Notifications;
using ProcessExplorer.Application.Common.Interfaces.Services;
using ProcessExplorer.Core.Enums;
using System;
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
        protected readonly INotificationService _notification;

        public CommunicationTypeCheckBehaviour(ICommunicationTypeClient communication,
            ICommunicationTypeService communicationTypeService,
            IInternet internet,
            ILoggerWrapper wrapper,
            IDateTime dateTime,
            ISessionService session,
            INotificationService notification)
        {
            _communication = communication;
            _communicationTypeService = communicationTypeService;
            _logger = wrapper;
            _time = dateTime;
            _internet = internet;
            _session = session;
            _notification = notification;
        }

        public async Task Check()
        {
            try
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
                    CommunicationType old = _communicationTypeService.GetCommunicationType();
                    _communicationTypeService.SetCommuncationType(type);
                    _notification.ShowStatusMessage(nameof(CommunicationTypeCheckBehaviour), $"Communication type changed from {old} to {type}");
                }

                _logger.LogInfo($"Finished fetching communication type: {_time.Now} | type is {type}");
            }
            catch (Exception e)
            {
                _logger.LogError(e);
            }
        }
    }
}
