using ProcessExplorer.Application.Common.Models;

namespace ProcessExplorer.Application.Common.Interfaces
{
    /// <summary>
    /// Singleton
    /// </summary>
    public interface IPlatformInformationService
    {
        PlatformInformation PlatformInformation { get; }
        void Set();
    }
}
