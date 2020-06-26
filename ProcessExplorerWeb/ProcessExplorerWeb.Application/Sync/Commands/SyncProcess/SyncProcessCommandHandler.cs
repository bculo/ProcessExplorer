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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncProcess
{
    public class SyncProcessCommandHandler : SyncRootHandler, IRequestHandler<SyncProcessCommand>
    {
        private readonly ILogger<SyncProcessCommandHandler> _logger;

        public SyncProcessCommandHandler(IUnitOfWork work,
            ICurrentUserService currentUser,
            IDateTime dateTime,
            ILogger<SyncProcessCommandHandler> logger) : base(work, currentUser, dateTime)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(SyncProcessCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncApplicationCommand handle for session {0} and user {1}", request.SessionId, _currentUser.UserId);

            //get session with given id and belongs to specific user
            var session = await _work.Session.GetSessionWithProcesses(request.SessionId, _currentUser.UserId);

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

            //get processes that are not yet stored in database
            var newProcesses = GetNewProcessesToStore(request?.Processes ?? EmptyList<ProcessInstanceDto>(), session.Processes);

            _logger.LogInformation("Session with given id {0} exists (UPDATE|INSERT...)", request.SessionId);

            if (newProcesses.Count > 0)
                _work.Process.AddRange(newProcesses);

            //save changes
            await _work.CommitAsync();

            _logger.LogInformation("Session with given id {0} exists (UPDATED|INSERTED)", request.SessionId);

            return Unit.Value;
        }
    }
}
