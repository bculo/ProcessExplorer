using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Session.Queries.GetSeletedSession
{
    public class GetSelectedSessionQueryResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public DateTime Started { get; set; }
    }
}
