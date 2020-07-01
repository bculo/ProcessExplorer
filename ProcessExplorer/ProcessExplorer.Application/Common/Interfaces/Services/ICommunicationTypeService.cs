using ProcessExplorer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Interfaces.Services
{
    public interface ICommunicationTypeService
    {
        CommunicationType GetCommunicationType();
        void SetCommuncationType(CommunicationType type);
    }
}
