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
    public class UserLogedOutEventHandler : INotificationHandler<UserLogedOutEvent>
    {
        private readonly INotificationService _service;

        public UserLogedOutEventHandler(INotificationService service)
        {
            _service = service;
        }

        public async Task Handle(UserLogedOutEvent notification, CancellationToken cancellationToken)
        {
            await _service.NotifyUserLogedOut();
        }
    }
}
