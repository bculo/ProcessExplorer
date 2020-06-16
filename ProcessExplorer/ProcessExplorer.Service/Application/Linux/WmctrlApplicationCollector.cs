using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;

namespace ProcessExplorer.Service.Application.Linux
{
    public class WmctrlApplicationCollector : RootAppCollector, IApplicationCollector
    {
        private readonly IDateTime _time;

        public WmctrlApplicationCollector(ILoggerWrapper logger, IDateTime time) : base(logger)
        { 
            _time = time;
        }

        public IList<ApplicationInformation> GetApplications()
        {
            _logger.LogInfo($"Started fetching applications in {nameof(WmctrlApplicationCollector)}");

            var startInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = $"-c \" wmctrl -lp \"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var process = new System.Diagnostics.Process();
            process.StartInfo = startInfo;
            process.Start();

            var terminalContent = process.StandardOutput.ReadToEnd();

            _logger.LogInfo($"Fetched content from terminal: {terminalContent}");
            var applicatioInfos = ParseApplicationNames(terminalContent).ToList();
            _logger.LogInfo($"Application fetched in {nameof(WmctrlApplicationCollector)} : {applicatioInfos.Count}");

            return applicatioInfos.ToList();
        }

        private IEnumerable<ApplicationInformation> ParseApplicationNames(string content) 
        {
            var terminalRows = content.Split(Environment.NewLine);
            
            foreach(var row in terminalRows)
            {
                var columns = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if(columns.Length < 4) 
                    continue;

                //PID
                var pidString = columns[2];

                //Windows handle
                var handle = columns[0];

                if(!int.TryParse(pidString, out int pidNum))
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

        private ApplicationInformation CreateAppInfoInstance(string longName, int pid)
        {
            var fetchedProcess = System.Diagnostics.Process.GetProcessById(pid);
            DateTime processStarted = fetchedProcess?.StartTime ?? _time.Now;
            return new ApplicationInformation
            {
                ApplicationName = GetBasicApplicationTitle(longName),
                StartTime = processStarted
            };
        }

        private int TryGetPidNum(string windowHandle)
        {
            var startInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = $"-c \" xprop -id {windowHandle} \"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var process = new System.Diagnostics.Process()
            {
                StartInfo = startInfo
            };
            process.Start();

            var terminalContent = process.StandardOutput.ReadToEnd();
            var pidRow = GetPidRow(terminalContent);

            var pidString = pidRow.Split("=", StringSplitOptions.RemoveEmptyEntries).Last();

            if(int.TryParse(pidString, out int pid))
                return pid;

            return -1;
        }

        private string GetPidRow(string terminalContent)
        {
            return terminalContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(i => i.Contains("_NET_WM_PID(CARDINAL)"));
        }
    }
}