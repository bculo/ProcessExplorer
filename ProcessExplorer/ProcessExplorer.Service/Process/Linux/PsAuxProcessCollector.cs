using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessExplorer.Service.Process.Linux
{
    public class PsAuxProcessCollector : LinuxRootProcessCollector, IProcessCollector
    {
        public PsAuxProcessCollector(ILoggerWrapper logger, 
            IPlatformInformationService info,
            ISessionService session) 
            : base(logger, session, info)
        {
        }
        
        public List<ProcessInformation> GetProcesses()
        {
            _logger.LogInfo($"Started fetching processes in {nameof(PsAuxProcessCollector)}");

            var startInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = $"-c \" ps axco user,pid,command \"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var process = new System.Diagnostics.Process();
            process.StartInfo = startInfo;
            process.Start();

            var terminalContent = process.StandardOutput.ReadToEnd();

            _logger.LogInfo($"Fetched content from terminal: {terminalContent}");

            IEnumerable<ProcessInformation> processesInfo = ParseProcessInformation(terminalContent);
            var processList = RemoveDuplicates(processesInfo).ToList();

            _logger.LogInfo($"Application fetched in {nameof(PsAuxProcessCollector)} : {processList.Count}");
            return processList;
        }

        private IEnumerable<ProcessInformation> ParseProcessInformation(string terminalContent)
        {
            string username = _info.PlatformInformation.UserName;
            var rowsWithSpecificUser = terminalContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                                .Skip(1) //skip header
                                                .Where(i => i.Contains(username));

            foreach(var row in rowsWithSpecificUser)
            {
                var rowColumns = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if(rowColumns.Length != 3)
                    continue;

                if(!int.TryParse(rowColumns[1], out int pid))
                    continue;

                yield return new ProcessInformation
                {
                    ProcessId = pid,
                    ProcessName = rowColumns[2]
                };
            }
        }
    }
}