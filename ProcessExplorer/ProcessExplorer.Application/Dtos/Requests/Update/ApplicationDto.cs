using System;

namespace ProcessExplorer.Application.Dtos.Requests.Update
{
    public class ApplicationDto
    {
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime LastUse { get; set; }
    }
}
