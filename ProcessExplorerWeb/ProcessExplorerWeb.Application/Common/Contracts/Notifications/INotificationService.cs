using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Contracts.Notifications
{
    public interface INotificationService
    {
        Task NotifyUserLogedIn();
        Task NotifyUserLogedOut();
        Task NotifySyncHappend(Guid userId);
    }
}
