using ProcessExplorer.Application.Common.Interfaces.Notifications;
using System;

namespace ProcessExplorer.Services
{
    public class ConsoleNotificationService : INotificationService
    {
        public void ShowStatusMessage(string from, string message)
        {
            Console.WriteLine($"__{from}__, {message}");
        }
    }
}
