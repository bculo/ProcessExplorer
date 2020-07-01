using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Events
{
    public class UserLogedInEvent : INotification
    {
        public UserLogedInEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
