using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ProcessExplorerWeb.Application.Events;
using ProcessExplorerWeb.Application.Sync.Commands.SyncApplication;
using ProcessExplorerWeb.Application.Sync.Commands.SyncProcess;
using ProcessExplorerWeb.Application.Sync.Commands.SyncSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.SignalR
{
    [Authorize]
    public class ProcessExplorerHub : Hub<IProcessExplorerHubClient>
    {
        private readonly IConnectionMapper<string> _connections;
        private readonly IMediator _mediator;

        public ProcessExplorerHub(IConnectionMapper<string> connections,
            IMediator mediator)
        {
            _connections = connections;
            _mediator = mediator;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task Subscribe()
        {
            //user unique identifier
            string identifier = Context.UserIdentifier;

            //check if that identifier is already in list of connections
            if (!_connections.GetConnections(identifier).Contains(Context.ConnectionId))
                _connections.Add(identifier, Context.ConnectionId);

            int connections = _connections.GetTotalConnections();

            await Clients.All.CreateNotificationForLogin(connections);
        }

        public async Task SyncSession(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            var command = JsonConvert.DeserializeObject<SyncSessionCommand>(json);

            await _mediator.Send(command);
        }

        public async Task SyncApplications(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            var command = JsonConvert.DeserializeObject<SyncApplicationCommand>(json);

            await _mediator.Send(command);
        }


        public async Task SynProcesses(string json)
        {
            if (json is null)
                throw new ArgumentNullException(nameof(json));

            var command = JsonConvert.DeserializeObject<SyncProcessCommand>(json);

            await _mediator.Send(command);
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //user unique identifier
            string identifier = Context.UserIdentifier;

            //remove specific connection
            if (_connections.Remove(identifier, Context.ConnectionId))
                await _mediator.Publish(new UserLogedOutEvent { });

            //call base function
            await base.OnDisconnectedAsync(exception);
        }
    }
}
