using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using ProcessExplorerWeb.Application.Sync.SharedLogic;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    class SyncSessionCommandHandler : SyncRootHandler, IRequestHandler<SyncSessionCommand>
    {
        private readonly ILogger<SyncSessionCommandHandler> _logger;

        public SyncSessionCommandHandler(IUnitOfWork work,
            ICurrentUserService currentUser,
            IDateTime dateTime,
            ILogger<SyncSessionCommandHandler> logger,
            IMediator mediator)
            : base(work, currentUser, dateTime, mediator)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(SyncSessionCommand request, CancellationToken cancellationToken)
        {
            HandleUserIdentifier(request);

            _logger.LogInformation("Starting to handle session {0} for user {1}", request.SessionId, UserId);

            //get session with given id and belongs to specific user
            var session = await _work.Session.GetSessionWithAppsAndProcesses(request.SessionId,UserId);

            //wrong request
            if (session != null && session.ExplorerUserId != UserId)
                throw new ProcessExplorerException("Invalid request");

            //Session not found, so create it
            if (session == null)
            {
                //Map to entity model
                session = request.Adapt<ProcessExplorerUserSession>();

                _logger.LogInformation("Session with given id {0} doesnt exists (INSERT...)", request.SessionId);

                await AddNewSession(session);

                _logger.LogInformation("Session with given id {0} doesnt exists (INSERTED)", request.SessionId);

                return Unit.Value;
            }

            _logger.LogInformation("Session with given id {0} exists (FILTERING...)", request.SessionId);

            //get new apps for this session and modify old entites if change detected
            //first parameter -> fetched apps
            //second parameter -> application from stored session
            var newApps = ModifyOldAndGetNewApps(request?.Applications ?? EmptyList<ApplicationInstanceDto>(), session.Applications, session.Id, out bool modificationHappend);

            //get processes that are not yet stored in database
            var newProcesses = GetNewProcessesToStore(request?.Processes ?? EmptyList<ProcessInstanceDto>(), session.Processes, session.Id);

            _logger.LogInformation("Session with given id {0} exists (UPDATE|INSERT...)", request.SessionId);

            //add apps
            if (newApps.Count > 0)
                _work.Applications.AddRange(newApps);

            //add processes
            if (newProcesses.Count > 0)
                _work.Process.AddRange(newProcesses);

            //save changes
            await SaveSyncChanges();

            _logger.LogInformation("Session with given id {0} exists (UPDATED|INSERTED)", request.SessionId);

            //success
            return Unit.Value;
        }
    }
}
