using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class SyncBehaviour : ISyncBehaviour
    {
        private readonly IInternet _internet;
        private readonly ISessionService _sessionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _time;
        private readonly ILoggerWrapper _logger;
        private readonly ISynchronizationClientFactory _factory;

        public SyncBehaviour(IInternet internet,
            IUnitOfWork unitOfWork,
            ILoggerWrapper logger,
            ISynchronizationClientFactory factory,
            ISessionService sessionService,
            IDateTime time)
        {
            _time = time;
            _internet = internet;
            _logger = logger;
            _factory = factory;
            _sessionService = sessionService;
            _unitOfWork = unitOfWork;
        }

        public async Task Synchronize()
        {
            _logger.LogInfo($"Sync behaviourr started: {_time.Now}");

            //Offline mode so exit
            if (_sessionService.SessionInformation.Offline)
            {
                _logger.LogInfo($"Sync finished: {_time.Now}, OFFLINE MODE");
                return;
            }

            //Check for internet connection
            if (!await _internet.CheckForInternetConnectionAsync())
            {
                _logger.LogInfo($"Sync finished: {_time.Now}, NO INTERNET ACCESS");
                return;
            }

            //Get all sessions from database
            var sessions = await _unitOfWork.Sessions.GetAllWithIncludesAsync();

            //no records in database so finish sync process
            if (sessions.Count == 0)
            {
                _logger.LogInfo($"Sync finished: {_time.Now}, NO RECORDS FOR SYNC");
                return;
            }
            
            //are we working only with current session ?
            if(sessions.Count == 1 && IsCurrentSession(sessions))
            {
                var curSession = sessions.First();

                if (!SessionNeedsUpdate(curSession))
                {
                    _logger.LogInfo($"Sync finished: {_time.Now}, NO RECORDS FOR SYNC");
                    return;
                }
            }

            //map entites from database to dtos
            var dtosSessions = sessions.Adapt<List<UserSessionDto>>();

            //list for successfuly synced entites
            var successStatuses = new List<UserSessionDto>();

            //sync process
            foreach(var session in dtosSessions)
            {
                //get sync client
                var syncClient = _factory.GetClient();

                //sync session
                var result = await syncClient.Sync(session);

                //if sync successful, add to list
                if (result)
                    successStatuses.Add(session);
            }

            //no success on sync
            if (successStatuses.Count == 0)
            {
                _logger.LogInfo($"Sync finished: {_time.Now}, NO SYNC");
                return;
            }

            /// Get successfuly synced sessions -> sessions that needs to be deleted
            var sessionsForDelete = GetSessionsForDelete(sessions, successStatuses);

            //remove current session entity -> current sessions = different treatment
            //get current session
            var currentSession = GetCurrentSessionFromList(sessionsForDelete);

            //remove if from deletesessions
            RemoveCurrentSessionFromList(sessionsForDelete);

            //handle current session
            HandleCurrentSession(currentSession);

            //zero objects, exit
            if (sessionsForDelete.Count() > 0)
            {
                //delete old sessions
                _unitOfWork.Sessions.RemoveRange(sessionsForDelete);
            }

            //save changes
            await _unitOfWork.CommitAsync();

            _logger.LogInfo($"Sync finished: {_time.Now}, SYNC");
        }

        /// <summary>
        /// Check if session needs update
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private bool SessionNeedsUpdate(Session session)
        {
            if (session.Applications.Count == 0 && session.ProcessEntities.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Are we working with current session
        /// </summary>
        /// <param name="sessions"></param>
        /// <returns></returns>
        private bool IsCurrentSession(List<Session> sessions)
        {
            Guid currentSessionId = _sessionService.SessionInformation.SessionId;
            //find current session
            return sessions.FirstOrDefault().Id == currentSessionId;
        }

        /// <summary>
        /// Logic for handleing current session instance
        /// </summary>
        /// <param name="currentSession"></param>
        private void HandleCurrentSession(Session currentSession)
        {
            //do nothing
            if (currentSession == null)
                return;

            //clean lists
            currentSession.Applications.Clear();
            currentSession.ProcessEntities.Clear();
        }

        /// <summary>
        /// Get current session (can be null)
        /// </summary>
        /// <param name="sessions">all stored sessions</param>
        /// <returns></returns>
        private Session GetCurrentSessionFromList(List<Session> sessions)
        {
            //get current session ID
            Guid currentSessionId = _sessionService.SessionInformation.SessionId;

            //find current session
            return sessions.SingleOrDefault(i => i.Id == currentSessionId);
        }

        /// <summary>
        /// Remove current session from list
        /// </summary>
        /// <param name="sessions"></param>
        private void RemoveCurrentSessionFromList(List<Session> sessions)
        {
            var currentSession = GetCurrentSessionFromList(sessions);
            if (currentSession != null)
                sessions.Remove(currentSession);
        }

        /// <summary>
        /// Get successfuly synced sessions -> sessions that needs to be deleted
        /// </summary>
        /// <param name="sessions"></param>
        /// <param name="succesfulySyncedSessions"></param>
        /// <returns></returns>
        private List<Session> GetSessionsForDelete(List<Session> sessions, List<UserSessionDto> succesfulySyncedSessions)
        {
            //get current session ID
            Guid currentSessionId = _sessionService.SessionInformation.SessionId;

            //create hashset
            HashSet<Guid> sessionsIds = new HashSet<Guid>(succesfulySyncedSessions.Select(i => i.SessionId));

            //get all sessions without current sessuin
            var sessionsForDelete = sessions.Where(i => sessionsIds.Contains(i.Id));
            return sessionsForDelete.ToList();
        }
    }
}
