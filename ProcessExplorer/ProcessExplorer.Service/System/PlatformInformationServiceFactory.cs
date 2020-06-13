using ProcessExplorer.Application.Common.Interfaces;

namespace ProcessExplorer.Service.Services.System
{
    public static class PlatformInformationServiceFactory
    {
        public static IPlatformInformationService CreatePlatformInformationService()
        {
            IPlatformInformationService service = null;

            service = new SystemInformationService();


            if (service != null)
                service.Set();

            return service;
        }
    }
}
