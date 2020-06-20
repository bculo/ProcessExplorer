using ProcessExplorer.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IUserSessionFactory
    {
        IUserSession GetUserSessionCollector();
    }
}
