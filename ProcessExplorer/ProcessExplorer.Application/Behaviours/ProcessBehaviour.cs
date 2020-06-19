using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ProcessBehaviour : IProcessBehaviour
    {
        public ProcessBehaviour()
        {

        }

        public Task Collect()
        {
            Console.WriteLine("COLLECTING PROCESSES");

            return Task.CompletedTask;
        }
    }
}
