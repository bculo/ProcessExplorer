using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ProcessExplorerWeb.Application.Events;
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

        public Task Subscribe()
        {
            //user unique identifier
            string identifier = Context.UserIdentifier;

            //check if that identifier is already in list of connections
            if (!_connections.GetConnections(identifier).Contains(Context.ConnectionId))
                _connections.Add(identifier, Context.ConnectionId);

            return Task.CompletedTask;
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
