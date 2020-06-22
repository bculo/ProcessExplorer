using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System.Collections.Generic;

namespace ProcessExplorer.Service.Process.Windows
{
    public abstract class WindowsRootProcessCollector : RootCollector
    {
        public WindowsRootProcessCollector(ILoggerWrapper logger, ISessionService session) : base(logger, session)
        {
        }

        public override IEnumerable<ProcessInformation> PlatformSpecificHandler(IEnumerable<ProcessInformation> processes) => processes;
    }
}
