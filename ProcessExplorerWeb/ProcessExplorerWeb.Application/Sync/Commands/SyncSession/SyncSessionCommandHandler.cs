using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Dtos.Models;
using ProcessExplorerWeb.Application.Dtos.Shared;
using ProcessExplorerWeb.Application.Extensions;
using ProcessExplorerWeb.Core.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    class SyncSessionCommandHandler : IRequestHandler<SyncSessionCommand>
    {
        private readonly IUnitOfWork _work;
        private readonly ICurrentUserService _currentUser;

        public SyncSessionCommandHandler(IUnitOfWork work,
            ICurrentUserService currentUser)
        {
            _work = work;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(SyncSessionCommand request, CancellationToken cancellationToken)
        {
            //get session if exists
            var session = await _work.Session.GetSessionWithWithAppsAndProcesses(request.SessionId, _currentUser.UserId);

            //Session not found so create it
            if(session == null)
            {
                var entity = request.Adapt<ProcessExplorerUserSession>();
                entity.ExplorerUserId = _currentUser.UserId;
                _work.Session.Add(entity);
                await _work.CommitAsync();
                return Unit.Value;
            }

            //get new apps for this session and modify old entites if needed
            var newApps = session.Applications.ModifyExistingAndGetNewApps(request.Applications.Cast<ApplicationExplorerModel>(), session.Id);

            //add new apps
            if(newApps.Count != 0)
                _work.Applications.BulkAdd(newApps);

            //Save modified app entites
            await _work.CommitAsync();

            //get processes that are not stored in database
            var processesToStore = session.Processes.GetNewProcesses(request.Processes.Cast<ProcessExplorerModel>(), session.Id);

            //map them to Entity
            if (processesToStore.Count != 0)
                _work.Process.BulkAdd(processesToStore);

            return Unit.Value;
        }
    }
}
