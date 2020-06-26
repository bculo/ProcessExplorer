using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncApplication
{
    public class SyncApplicationCommandHandler : IRequestHandler<SyncApplicationCommand>
    {
        private readonly IUnitOfWork _work;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public SyncApplicationCommandHandler(IUnitOfWork work,
            ICurrentUserService currentUser,
            IDateTime dateTime)
        {
            _work = work;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(SyncApplicationCommand request, CancellationToken cancellationToken)
        {
            //get session with given id and belongs to specific user
            var session = await _work.Session.GetAsync(request.SessionId);

            if (session != null && session.ExplorerUserId != _currentUser.UserId)
                throw new ProcessExplorerException("Invalid request");

            /*
            //Session not found, so create it
            if (session == null)
            {
                //Map to entity model
                var entity = request.Adapt<ProcessExplorerUserSession>();

                //set user id on session instance
                SetUserIdOnSession(entity);

                //set inserted time
                entity.Inserted = _dateTime.Now;

                //add to database
                _work.Session.Add(entity);
                await _work.CommitAsync();

                //exit
                return Unit.Value;
            }

            //get new apps for this session and modify old entites if change detected
            //first parameter -> fetched apps
            //second parameter -> application from stored session
            var newApps = ModifyOldAndGetNewApps(request?.Applications ?? EmptyList<SyncSessionApplicationInfoCommand>(), session.Applications, out bool modificationHappend);

            //get processes that are not yet stored in database
            var newProcesses = GetNewProcessesToStore(request?.Processes ?? EmptyList<SyncSessionProcessInfoCommand>(), session.Processes);

            //add apps
            if (newApps.Count > 0)
                _work.Applications.AddRange(newApps);

            //add processes
            if (newProcesses.Count > 0)
                _work.Process.AddRange(newProcesses);

            //save changes
            await _work.CommitAsync();

            */
            //success
            return Unit.Value;
        }
    }
}
