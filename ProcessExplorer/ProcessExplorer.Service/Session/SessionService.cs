using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;

namespace ProcessExplorer.Service.Session
{
    public class SessionService : ISessionService
    {
        private readonly IUserSessionFactory _sessionFactory;

        public SessionInformation SessionInformation { get; private set; }

        public SessionService(IUserSessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;

            SetSession();
        }

        /// <summary>
        /// Set session
        /// </summary>
        private void SetSession()
        {
            SessionInformation = _sessionFactory.GetUserSessionCollector().GetSession();
        }

        /// <summary>
        /// Change session id
        /// </summary>
        /// <param name="sessionId"></param>
        public void ChangeSessionId(Guid sessionId)
        {
            SessionInformation.SessionId = sessionId;
        }
    }
}
