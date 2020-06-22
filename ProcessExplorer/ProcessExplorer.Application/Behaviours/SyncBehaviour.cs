using Mapster;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class SyncBehaviour : ISyncBehaviour
    {
        private readonly IInternet _internet;
        private readonly ISessionService _sessionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISynchronizationClientFactory _factory;

        public SyncBehaviour(IInternet internet,
            IUnitOfWork unitOfWork,
            ISynchronizationClientFactory factory,
            ISessionService sessionService)
        {
            _internet = internet;
            _unitOfWork = unitOfWork;
            _factory = factory;
            _sessionService = sessionService;
        }

        public async Task Synchronize()
        {
            if (_sessionService.SessionInformation.Offline)
                return;

            //Check for internet connection
            if (!await _internet.CheckForInternetConnectionAsync())
                return;

            //Get all sessions
            Guid currentSession = _sessionService.SessionInformation.SessionId;
            var sessions = await _unitOfWork.Sessions.GetAllWithIncludesWihoutCurrentAsync(currentSession);

            //map to Dtos
            var dtos = sessions.Adapt<List<UserSessionDto>>();

            //guid is sessionId
            //bool update status from server
            var bagSuccessSessions = new ConcurrentBag<UserSessionDto>();

            //Synchronize with backend
            Parallel.ForEach(dtos, async session =>
            {
                var syncClient = _factory.GetClient();
                bool success = await syncClient.Sync(session);
                if(success)
                    bagSuccessSessions.Add(session);
            });

            var updatedSessions = bagSuccessSessions.ToList();
            var sessionsForDelete = sessions.Where(i => updatedSessions.Any(p => p.SessionId == i.Id));

            _unitOfWork.Sessions.RemoveRange(sessionsForDelete);
            await _unitOfWork.CommitAsync();
        }
    }
}
