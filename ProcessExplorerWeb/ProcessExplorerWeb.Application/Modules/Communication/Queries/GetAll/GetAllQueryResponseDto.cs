using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Communication.Queries.GetAll
{
    public class GetAllQueryResponseDto
    {
        public IEnumerable<CommunicationInstanceType> Types { get; set; }
    }

    public class CommunicationInstanceType 
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
