using ProcessExplorer.Application.Common.Interfaces.Services;
using ProcessExplorer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Service.Communication
{
    /// <summary>
    /// SINGLETON
    /// </summary>
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
