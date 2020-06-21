﻿using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorer.Service.Application.Windows
{
    public class WindowsViaProcessApplicationCollector : RootAppCollector, IApplicationCollector
    {
        public WindowsViaProcessApplicationCollector(ILoggerWrapper logger, ISessionService sessionService, IDateTime date) 
            : base(logger, sessionService, date)
        {
        }

        public List<ApplicationInformation> GetApplications()
        {
            _logger.LogInfo($"Started fetching applications in {nameof(WindowsViaProcessApplicationCollector)}");

            var appList = new List<ApplicationInformation>();
            foreach(var item in GetProcesses())
            {
                appList.Add(new ApplicationInformation
                {
                    StartTime = item.StartTime,
                    ApplicationName = GetBasicApplicationTitle(item.MainWindowTitle),
                    Session = _sessionService.SessionInformation.SessionId,
                    FetchTime = _dateTime.Now
                });
            }

            _logger.LogInfo($"Application fetched in {nameof(WindowsViaProcessApplicationCollector)} : {appList.Count}");
            return appList;
        }


        private IEnumerable<System.Diagnostics.Process> GetProcesses()
        {
            return System.Diagnostics.Process.GetProcesses()
                        .Where(i => i.MainWindowHandle != IntPtr.Zero);
        }
    }
}
