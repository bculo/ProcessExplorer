using MediatR;
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
        public UserLogedInEventHandler()
        {

        }

        public Task Handle(UserLogedInEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
