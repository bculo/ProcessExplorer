using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.SignalR
{
    [Authorize]
    public class ProcessExplorerHub : Hub
    {
        private readonly IConnectionMapper<string> _connections;

        public ProcessExplorerHub(IConnectionMapper<string> connections)
        {
            _connections = connections;
        }

        public override Task OnConnectedAsync()
        {
            //user unique identifier
            string identifier = Context.UserIdentifier;

            //check if that identifier is already in list of connections
            if (!_connections.GetConnections(identifier).Contains(Context.ConnectionId))
                _connections.Add(identifier, Context.ConnectionId);

            //call base function
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //user unique identifier
            string identifier = Context.UserIdentifier;

            //remove specific connection
            _connections.Remove(identifier, Context.ConnectionId);

            //call base function
            return base.OnDisconnectedAsync(exception);
        }
    }
}
