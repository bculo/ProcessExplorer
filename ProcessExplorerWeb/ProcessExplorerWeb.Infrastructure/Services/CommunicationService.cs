using ProcessExplorerWeb.Application.Common.Contracts.Services;
using ProcessExplorerWeb.Application.Common.Enums;

namespace ProcessExplorerWeb.Infrastructure.Services
{
    public class CommunicationService : ICommunicationTypeService
    {
        private readonly object _lock = new object();

        /// <summary>
        /// Default communication is REST 
        /// </summary>
        private CommunicationType Type { get; set; } = CommunicationType.REST;

        public CommunicationType GetCommunicationType()
        {
            lock (_lock)
            {
                return Type;
            }
        }

        public void SetCommuncationType(CommunicationType type)
        {
            lock (_lock)
            {
                Type = type;
            }
        }
    }
}
