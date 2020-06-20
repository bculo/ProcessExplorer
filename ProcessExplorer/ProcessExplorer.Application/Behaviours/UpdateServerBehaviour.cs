using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Linq;
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

        public async Task Update()
        {
            //Check for internet connection
            if (!await _internet.CheckForInternetConnectionAsync())
                return;

            //Get all sessions
            var sessions = _unitOfWork.Sessions.GetAll().ToList();

            Parallel.ForEach(sessions, s =>
            {
                //TODO push each session to backend

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
        }
    }
}
