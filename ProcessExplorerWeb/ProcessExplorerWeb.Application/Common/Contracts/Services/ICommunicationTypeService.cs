using ProcessExplorerWeb.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Contracts.Services
{
    public interface ICommunicationTypeService
    {
        CommunicationType GetCommunicationType();
        void SetCommuncationType(CommunicationType type);
    }
}
