using Mapster;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Authentication;
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
            var sessions = await _unitOfWork.Sessions.GetAllWithIncludesAsync();

            //map to Dtos
            var dtosSessions = sessions.Adapt<List<UserSessionDto>>();

            var successStatuses = new List<UserSessionDto>();
            foreach(var session in dtosSessions)
            {
                var syncClient = _factory.GetClient();
                var result = await syncClient.Sync(session);
                if (result)
                    successStatuses.Add(session);
            }

            //get sessions that we need to delete
            Guid currentSessionId = _sessionService.SessionInformation.SessionId;
            var sessionsForDelete = sessions.Where(i => i.Id != currentSessionId && successStatuses.Any(p => p.SessionId == i.Id)).ToList();

            if (sessionsForDelete.Count() == 0)
                return;

            //Delete sessions
            _unitOfWork.Sessions.RemoveRange(sessionsForDelete);
            await _unitOfWork.CommitAsync();
        }
    }
}
