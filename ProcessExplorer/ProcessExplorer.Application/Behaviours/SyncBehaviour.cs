using Mapster;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class SyncBehaviour : ISyncBehaviour
    {
        private readonly IInternet _internet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISynchronizationClientFactory _factory;

        public SyncBehaviour(IInternet internet,
            IUnitOfWork unitOfWork,
            ISynchronizationClientFactory factory)
        {
            _internet = internet;
            _unitOfWork = unitOfWork;
            _factory = factory;
        }

        public async Task Synchronize()
        {
            //Check for internet connection
            if (!await _internet.CheckForInternetConnectionAsync())
                return;

            //Get all sessions
            var sessions = await _unitOfWork.Sessions.GetAllWithIncludesAsync();

            //map to Dtos
            var dtos = sessions.Adapt<List<UserSessionDto>>();

            string json = JsonConvert.SerializeObject(dtos);

            //guid is sessionId
            //bool update status from server
            var dictionary = new ConcurrentDictionary<Guid, bool>();

            //Synchronize with backend
            Parallel.ForEach(dtos, async s =>
            {
                var syncClient = _factory.GetClient();
                bool success = await syncClient.SyncSession(s);
                dictionary.TryAdd(s.SessionId, success);
            });

            Console.WriteLine();
        }
    }
}
