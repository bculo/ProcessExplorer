using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;

namespace ProcessExplorer.Service.Session
{
    public class SessionService : ISessionService
    {
        private readonly IUserSessionFactory _sessionFactory;

        public SessionInformation SessionInformation { get; private set; }

        public SessionService(IUserSessionFactory sessionFactory,
            IPlatformInformationService service)
        {
            _sessionFactory = sessionFactory;

            SetSession(service.PlatformInformation.Type);
        }

        /// <summary>
        /// Set session
        /// </summary>
        private void SetSession(Platform type)
        {
            SessionInformation = _sessionFactory.GetUserSessionCollector().GetSession();
            SessionInformation.OS = type.ToString();
            SessionInformation.Offline = false;
        }

        /// <summary>
        /// Change session id
        /// </summary>
        /// <param name="sessionId"></param>
        public void ChangeSessionId(Guid sessionId)
        {
            SessionInformation.SessionId = sessionId;
        }

        /// <summary>
        /// Set current mode
        /// </summary>
        /// <param name="mode"></param>
        public void SetMode(WorkMode mode)
        {
            SessionInformation.Offline = (mode == WorkMode.OFFLINE) ? true : false;
        }
    }
}
