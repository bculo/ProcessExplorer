using System;
using System.Collections.Generic;

namespace ProcessExplorer.Application.Dtos.Requests.Update
{
    public class UserSessionDto
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public IEnumerable<ProcessDto> Processes { get; set; }
        public IEnumerable<ApplicationDto> Applications { get; set; }
    }
}
