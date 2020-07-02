using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Events
{
    public class SyncHappendEvent : INotification
    {
        public Guid UserID { get; set; }

        public SyncHappendEvent(Guid userId)
        {
            UserID = userId;
        }
    }
}
