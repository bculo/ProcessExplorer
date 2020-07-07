using System;
using System.Linq;
using System.Diagnostics;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;

namespace ProcessExplorer.Service.Session.Linux
{
    public class LinuxSession : IUserSession
    {
        private readonly ILoggerWrapper _logger;
        private readonly IPlatformInformationService _platformInformation;

        public LinuxSession(ILoggerWrapper logger, IPlatformInformationService platformInformation)
        {
            _logger = logger;
            _platformInformation = platformInformation;
        }

        public SessionInformation GetSession()
        {
            _logger.LogInfo($"Session collector | {nameof(LinuxSession)}");

            var startInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = $"-c \" who \"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var process = new System.Diagnostics.Process()
            {
                StartInfo = startInfo
            };
            process.Start();

            var terminalContent = process.StandardOutput.ReadToEnd();
            DateTime logonTime = GetLogonTime(terminalContent).ToUniversalTime();

            var userName = Environment.UserName;

            return new SessionInformation 
            {
                SessionId = Guid.NewGuid(),
                SessionStarted = logonTime,
                User = _platformInformation.PlatformInformation.UserName
            };
        }

        private DateTime GetLogonTime(string terminalContent)
        {
            var columns = terminalContent.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            if (columns.Count < 4)
                throw new FormatException("Length must be at least 4");

            var logonTimeColumns = columns.Skip(2).Take(2);
            var logonTimeString = string.Join(" ", logonTimeColumns);

            if (!DateTime.TryParse(logonTimeString, out DateTime logonTime))
                throw new FormatException("Wrong date formt");

            return logonTime;
        }
    }
}