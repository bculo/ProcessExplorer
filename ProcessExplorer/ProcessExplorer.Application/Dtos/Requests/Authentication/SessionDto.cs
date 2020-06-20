using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Dtos.Requests.Authentication
{
    public class SessionDto
    {
        public Guid SesssionId { get; set; }
        public string UserName { get; set; }
        public DateTime Started { get; set; }
    }
}
