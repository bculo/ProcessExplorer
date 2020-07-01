using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IInstallation
    {
        IServiceCollection Configure(IServiceCollection services, IConfiguration config);
    }
}
