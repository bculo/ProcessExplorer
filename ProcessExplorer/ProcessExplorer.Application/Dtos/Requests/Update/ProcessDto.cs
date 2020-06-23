using System;

namespace ProcessExplorer.Application.Dtos.Requests.Update
{
    public class ProcessDto
    {
        public string Name { get; set; }
        public DateTime Detected { get; set; }
        public Guid SessionId { get; set; }
    }
}
