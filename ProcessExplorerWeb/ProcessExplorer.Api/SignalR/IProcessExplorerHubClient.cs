using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.SignalR
{
    public interface IProcessExplorerHubClient
    {
        Task CreateNotificationForLogin();
        Task CreateNotificationForLogout();
        Task CreateSyncNotification();
    }
}
