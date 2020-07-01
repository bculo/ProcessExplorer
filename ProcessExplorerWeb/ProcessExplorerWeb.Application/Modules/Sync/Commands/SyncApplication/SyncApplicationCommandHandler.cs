﻿using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using ProcessExplorerWeb.Application.Sync.SharedLogic;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncApplication
{
    public class SyncApplicationCommandHandler : SyncRootHandler, IRequestHandler<SyncApplicationCommand>
    {
        private readonly ILogger<SyncApplicationCommandHandler> _logger;

        public SyncApplicationCommandHandler(IUnitOfWork work,
            ICurrentUserService currentUser,
            IDateTime dateTime,
            ILogger<SyncApplicationCommandHandler> logger) 
            : base (work, currentUser, dateTime)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(SyncApplicationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncApplicationCommand handle for session {0} and user {1}", request.SessionId, _currentUser.UserId);

            //get session with given id and belongs to specific user
            var session = await _work.Session.GetSessionWithApps(request.SessionId, _currentUser.UserId);

            if (session != null && session.ExplorerUserId != _currentUser.UserId)
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

            _logger.LogInformation("Session with given id {0} exists (UPDATE|INSERT...)", request.SessionId);

            //add apps
            if (newApps.Count > 0)
                _work.Applications.AddRange(newApps);

            //save changes
            await _work.CommitAsync();

            _logger.LogInformation("Session with given id {0} (UPDATED|INSERTED)", request.SessionId);

            //success
            return Unit.Value;
        }
    }
}