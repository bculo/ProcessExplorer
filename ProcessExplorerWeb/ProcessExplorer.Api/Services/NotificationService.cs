using Microsoft.AspNetCore.SignalR;
using ProcessExplorer.Api.SignalR;
using ProcessExplorerWeb.Application.Common.Contracts.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<ProcessExplorerHub, IProcessExplorerHubClient> _hub;
        private IConnectionMapper<string> _connections;

        public NotificationService(IConnectionMapper<string> connectionMapper,
             IHubContext<ProcessExplorerHub, IProcessExplorerHubClient> hub)
        {
            _hub = hub;
            _connections = connectionMapper;
        }

        public async Task NotifySyncHappend(Guid userId)
        {
            var connectionsForUser = _connections.GetConnections(userId.ToString());

            await _hub.Clients.Clients(connectionsForUser.ToList().AsReadOnly()).CreateSyncNotification();
        }

        public async Task NotifyUserLogedIn()
        {
            await _hub.Clients.All.CreateNotificationForLogin();
        }

        public async Task NotifyUserLogedOut()
        {
            await _hub.Clients.All.CreateNotificationForLogout();
        }
    }
}
