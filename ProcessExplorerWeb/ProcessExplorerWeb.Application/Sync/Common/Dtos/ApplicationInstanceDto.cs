using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Sync.SharedDtos
{
    public class ApplicationInstanceDto
    {
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime LastUse { get; set; }
        public Guid SessionId { get; set; }
    }
}
