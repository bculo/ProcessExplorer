using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Interfaces.Notifications
{
    public interface INotificationService
    {
        void DisplayMessage(string from, string message);
    }
}
