using ProcessExplorer.Application.Common.Extensions;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace ProcessExplorer.Service.Session.Windows
{
    public class WindowsSession : IUserSession
    {
        private readonly ILoggerWrapper _logger;
        private readonly IPlatformInformationService _platformInformation;

        public WindowsSession(ILoggerWrapper logger, IPlatformInformationService platformInformation)
        {
            _logger = logger;
            _platformInformation = platformInformation;
        }

        public SessionInformation GetSession()
        {
            _logger.LogInfo($"Session collector | {nameof(WindowsSession)}");

            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c query user",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var process = new System.Diagnostics.Process
            {
                StartInfo = startInfo
            };
            process.Start();

            var cmdContent = process.StandardOutput.ReadToEnd();
            DateTime logonTime = GetLogonTime(cmdContent).ToUniversalTime();

            var sessionInstance = new SessionInformation
            {
                SessionId = Guid.NewGuid(),
                SessionStarted = logonTime,
                User = _platformInformation.PlatformInformation.UserName
            };

            _logger.LogInfo("User session created {@data} | WindowsSession", sessionInstance);
            return sessionInstance;
        }

        private DateTime GetLogonTime(string content)
        {
            var rows = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length != 2)
                throw new FormatException("Length must be 2");

            //Index 0 is header
            var rowsWithuotHeader = rows[1];

            //content exemple: >božo  console  4  Active  17:40  16.6.2020. 5:42
            var columns = rowsWithuotHeader.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var logonTimeColumns = columns.Skip(5);
            var logonTimeString = string.Join(" ", logonTimeColumns);

            if (!DateTime.TryParse(logonTimeString, out DateTime logonTime))
                throw new FormatException("Wrong date formt");

            return logonTime;
        }
    }
}
