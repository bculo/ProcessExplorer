using System;

namespace ProcessExplorerWeb.Application.Sync.SharedDtos
{
    public class ProcessInstanceDto
    {
        public string Name { get; set; }
        public DateTime Detected { get; set; }
        public Guid SessionId { get; set; }
    }
}
