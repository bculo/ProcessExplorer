using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessExplorer.Service.Application.Linux
{
    public class WmctrlApplicationCollector : LinuxRootAppCollector, IApplicationCollector
    {
        public WmctrlApplicationCollector(ILoggerWrapper logger, 
            IDateTime time,
            ISessionService sessionService) : base(logger, sessionService, time)
        { 
        }

        /// <summary>
        /// Get currently opened applications using command ( wmctrl -lp )
        /// </summary>
        /// <returns></returns>
        public List<ApplicationInformation> GetApplications()
        {
            _logger.LogInfo($"Started fetching applications in {nameof(WmctrlApplicationCollector)}");

            //wmctrl -lp
            var startInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = $"-c \" wmctrl -lp \"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            //start process
            var process = new System.Diagnostics.Process
            {
                StartInfo = startInfo
            };
            process.Start();

            //read content
            var terminalContent = process.StandardOutput.ReadToEnd();

            //parse content
            _logger.LogInfo($"Fetched content from terminal: {terminalContent}");
            var applicatioInfos = ParseApplicationNames(terminalContent).ToList();
            _logger.LogInfo($"Application fetched in {nameof(WmctrlApplicationCollector)} : {applicatioInfos.Count}");

            return applicatioInfos.ToList();
        }

        /// <summary>
        /// Parse content fetched from terminal
        /// Format example: 0x04000003 -1 1569   bozo-Aspire-F5-571G Desktop
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private IEnumerable<ApplicationInformation> ParseApplicationNames(string content) 
        {
            //get rows
            var terminalRows = content.Split(Environment.NewLine);
            
            //go throught each row
            foreach(var row in terminalRows)
            {
                //split row content
                var columns = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if(columns.Length < 4) 
                    continue;

                //Windows handle
                var handle = columns[0];

                //PID
                var pidString = columns[2];

                //NOTE: some application dont return PID when using wmctrl -lp, sto try another method
                if (!int.TryParse(pidString, out int pidNum))
                    pidNum = TryGetPidNum(handle);

                //Skip first 4 records, last record is app name
                //0x04000003 -1 1569   bozo-Aspire-F5-571G Desktop
                var appNameParts = columns.Skip(4).ToList();

                if(appNameParts.Count == 1)
                    yield return CreateAppInfoInstance(appNameParts.First(), pidNum);
                
                string fullAppName = string.Join(" ", appNameParts);
                yield return CreateAppInfoInstance(fullAppName, pidNum);
            }
        }

        /// <summary>
        /// Create ApplicationInformation instance
        /// </summary>
        /// <param name="longName"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        private ApplicationInformation CreateAppInfoInstance(string longName, int pid)
        {
            //fetch process using GetProcessById method
            var fetchedProcess = System.Diagnostics.Process.GetProcessById(pid);

            //if process is null, set start time to current time
            DateTime processStarted = fetchedProcess?.StartTime ?? _dateTime.Now;

            return new ApplicationInformation
            {
                ApplicationName = GetBasicApplicationTitle(longName, fetchedProcess?.ProcessName),
                StartTime = processStarted.ToUniversalTime(),
                Session = _sessionService.SessionInformation.SessionId,
                FetchTime = processStarted.ToUniversalTime()
            };
        }

        /// <summary>
        /// Get Windows PID using windowhandle (fetched from wmctrl -lp command)
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <returns></returns>
        private int TryGetPidNum(string windowHandle)
        {
            //use commnad xprop -id <window handle>
            var startInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = $"-c \" xprop -id {windowHandle} \"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            //start process
            var process = new System.Diagnostics.Process()
            {
                StartInfo = startInfo
            };
            process.Start();

            //read content
            var terminalContent = process.StandardOutput.ReadToEnd();

            //Get row where PID is defiend
            var pidRow = GetPidRow(terminalContent);

            //PID row example _NET_WM_PID(CARDINAL)=517
            var pidString = pidRow.Split("=", StringSplitOptions.RemoveEmptyEntries).Last();

            //return PID
            if(int.TryParse(pidString, out int pid))
                return pid;

            //error
            return -1;
        }

        /// <summary>
        /// Get first row that contains (_NET_WM_PID(CARDINAL)")
        /// </summary>
        /// <param name="terminalContent"></param>
        /// <returns></returns>
        private string GetPidRow(string terminalContent)
        {
            return terminalContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(i => i.Contains("_NET_WM_PID(CARDINAL)"));
        }
    }
}