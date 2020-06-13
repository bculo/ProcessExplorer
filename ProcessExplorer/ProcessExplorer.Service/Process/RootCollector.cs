using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorer.Service.Process
{
    public abstract class RootCollector
    {
        protected readonly IPlatformProcessRecognizer _recognizer;

        protected RootCollector(IPlatformProcessRecognizer recognizer)
        {
            _recognizer = recognizer;
        }

        protected IEnumerable<ProcessInformation> FilterPlatformProcesses(IEnumerable<ProcessInformation> processes)
        {
            return processes.Where(i => !_recognizer.IsPlatfromProcess(i?.ProcessPath));
        }
    }
}
