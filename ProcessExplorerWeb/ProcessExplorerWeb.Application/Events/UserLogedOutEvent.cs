using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Events
{
    public class UserLogedOutEvent : INotification
    {
        public UserLogedOutEvent()
        {

        }
    }
}
