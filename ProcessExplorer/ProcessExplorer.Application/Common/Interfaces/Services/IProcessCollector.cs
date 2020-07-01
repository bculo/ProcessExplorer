using ProcessExplorer.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IProcessCollector
    {
        List<ProcessInformation> GetProcesses();
    }
}
