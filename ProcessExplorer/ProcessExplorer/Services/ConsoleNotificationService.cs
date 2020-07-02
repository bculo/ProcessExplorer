using ProcessExplorer.Application.Common.Interfaces.Notifications;
using System;

namespace ProcessExplorer.Services
{
    public class ConsoleNotificationService : INotificationService
    {
        public void DisplayMessage(string from, string message)
        {
            Console.WriteLine($"__{from}__, {message}");
        }
    }
}
