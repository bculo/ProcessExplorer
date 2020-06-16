using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IUserSession
    {
        SessionInformation GetSession();
    }
}
