using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;

namespace ProcessExplorer.Service.Application.Linux
{
    public class TerminalApplicationCollector : RootAppCollector, IApplicationCollector
    {
        public TerminalApplicationCollector(ILoggerWrapper logger) : base(logger)
        { 
        }

        public IList<ApplicationInformation> GetApplications()
        {
            _logger.LogInfo($"Started fetching applications in {nameof(TerminalApplicationCollector)}");

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
            if(string.IsNullOrEmpty(terminalContent))
                return new List<ApplicationInformation>();

            _logger.LogInfo($"Fetched content from terminal: {terminalContent ?? string.Empty}");
            var applicationNames = ParseApplicationNames(terminalContent).ToList();
            _logger.LogInfo($"Application fetched in {nameof(TerminalApplicationCollector)} : {applicationNames.Count}");
            
            return applicationNames.Select(name => new ApplicationInformation 
            {
                ApplicationName = GetBasicApplicationTitle(name)
            }).ToList();
        }

        private IEnumerable<string> ParseApplicationNames(string content) 
        {
            var terminalRows = content.Split(Environment.NewLine);
            
            foreach(var row in terminalRows)
            {
                var columns = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if(columns.Length < 4) 
                    continue;

                //Skip first 4 records, last record is app name
                //0x04000003 -1 1569   bozo-Aspire-F5-571G Desktop
                var appNameParts = columns.Skip(4).ToList();

                if(appNameParts.Count == 1)
                    yield return appNameParts.First();
                else
                    yield return string.Join(" ", appNameParts);
            }
        }
    }
}