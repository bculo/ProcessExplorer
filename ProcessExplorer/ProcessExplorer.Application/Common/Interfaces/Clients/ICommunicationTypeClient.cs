using ProcessExplorer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces.Clients
{
    public interface ICommunicationTypeClient
    {
        Task<CommunicationType> GetCommunicationType();
    }
}
