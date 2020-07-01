using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ProcessExplorer.Application.Common.Interfaces
{
    /// <summary>
    /// Configuration installer
    /// </summary>
    public interface IInstallation
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
