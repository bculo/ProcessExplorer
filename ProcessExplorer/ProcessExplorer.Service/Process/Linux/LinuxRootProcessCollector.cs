using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorer.Service.Process.Linux
{
    public abstract class LinuxRootProcessCollector : RootCollector
    {
        protected readonly IPlatformInformationService _info;

        public LinuxRootProcessCollector(ILoggerWrapper logger, ISessionService session, IPlatformInformationService info) : base(logger, session)
        {
            _info = info;
        }

        public override IEnumerable<ProcessInformation> PlatformSpecificHandler(IEnumerable<ProcessInformation> processes)
        {
            //remove all processes that have 50 or more characters
            return processes.Where(i => i.ProcessName.Length < 50);
        }
    }
}
