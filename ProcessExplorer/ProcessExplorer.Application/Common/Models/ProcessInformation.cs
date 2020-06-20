using System;
using System.Linq;

namespace ProcessExplorer.Application.Common.Models
{
    public class ProcessInformation
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string ProcessNameNormalized => ProcessName.ToUpper();
        public string ProcessPath { get; set; }
        public Guid Session { get; set; }
    }
}
