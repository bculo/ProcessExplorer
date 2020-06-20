using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class UpdateServerBehaviour : IUpdateBehaviour
    {
        private readonly IInternet _internet;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServerBehaviour(IInternet internet,
            IUnitOfWork unitOfWork)
        {
            _internet = internet;
            _unitOfWork = unitOfWork;
        }

        public async Task CheckForUpdates()
        {
            //Check for internet connection
            if (!await _internet.CheckForInternetConnectionAsync())
                return;

            //Get all sessions
            var sessions = await _unitOfWork.Sessions.GetAllWithIncludesAsync();

            //map to Dtos
            var result = sessions.Adapt<List<UserSessionDto>>();

            Parallel.ForEach(sessions, s =>
            {
                //TODO push each session to backend

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
        }
    }
}
