using ProcessExplorer.Application.Common.Enums;

namespace ProcessExplorer.Application.Common.Models
{
    public sealed class PlatformInformation
    {
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string UserDomainName { get; set; }
        public string Platform { get; set; }
        public Platform Type { get; set; }
        public string PlatformVersion { get; set; }
    }
}
