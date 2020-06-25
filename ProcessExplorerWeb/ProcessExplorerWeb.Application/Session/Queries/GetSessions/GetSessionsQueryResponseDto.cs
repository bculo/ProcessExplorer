using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSessions
{
    public class GetSessionsQueryResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public DateTime Started { get; set; }
        public int DifferentProcessesNumber { get; set; }
        public int ApplicationNumber { get; set; }
    }
}
