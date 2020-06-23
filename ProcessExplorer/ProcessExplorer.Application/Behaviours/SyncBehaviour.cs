using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
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

            //get current session ID
            Guid currentSessionId = _sessionService.SessionInformation.SessionId;

            //filter all successfuly synched entites
            //remove current session entity
            var sessionsForDelete = sessions.Where(i => i.Id != currentSessionId && successStatuses.Any(p => p.SessionId == i.Id));

            //zero objects, exit
            if (sessionsForDelete.Count() == 0)
            {
                _logger.LogInfo($"Sync finished: {_time.Now}, NO SYNC");
                return;
            }

            //delete old sessions
            _unitOfWork.Sessions.RemoveRange(sessionsForDelete);
            await _unitOfWork.CommitAsync();

            _logger.LogInfo($"Sync finished: {_time.Now}, SYNC");
        }
    }
}
