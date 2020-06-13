using ProcessExplorer.Application.Common.Interfaces;

namespace ProcessExplorer.Service.Services.System
{
    public static class PlatformInformationServiceFactory
    {
        public static IPlatformInformationService CreatePlatformInformationService()
        {
            IPlatformInformationService service = new SystemInformationService();
            service.Set();
            return service;
        }
    }
}
