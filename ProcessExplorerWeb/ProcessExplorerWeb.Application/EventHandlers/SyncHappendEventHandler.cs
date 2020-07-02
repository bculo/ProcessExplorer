using MediatR;
using ProcessExplorerWeb.Application.Common.Contracts.Notifications;
using ProcessExplorerWeb.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.EventHandlers
{
    public class SyncHappendEventHandler : INotificationHandler<SyncHappendEvent>
    {
        private readonly INotificationService _service;

        public SyncHappendEventHandler(INotificationService service)
        {
            _service = service;
        }

        public async Task Handle(SyncHappendEvent notification, CancellationToken cancellationToken)
        {
            await _service.NotifySyncHappend(notification.UserID);
        }
    }
}
