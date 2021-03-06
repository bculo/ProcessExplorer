﻿using MediatR;
using ProcessExplorerWeb.Application.Common.Contracts.Notifications;
using ProcessExplorerWeb.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.EventHandlers
{
    public class UserLogedInEventHandler : INotificationHandler<UserLogedInEvent>
    {
        private readonly INotificationService _service;

        public UserLogedInEventHandler(INotificationService service)
        {
            _service = service;
        }

        public async Task Handle(UserLogedInEvent notification, CancellationToken cancellationToken)
        {
            await _service.NotifyUserLogedIn();
        }
    }
}
