using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Application.Windows;
using System;
using System.Collections.Generic;

namespace ProcessExplorer.Service.Application
{
    public class ApplicationCollectorFactory : IApplicationCollectorFactory
    {
        private static Dictionary<Platform, Func<IApplicationCollector>> ExecutionMethods;

        private readonly ILoggerWrapper _logger;
        private readonly IPlatformInformationService _platform;

        public ApplicationCollectorFactory(ILoggerWrapper logger,
            IPlatformInformationService platform)
        {
            _logger = logger;
            _platform = platform;
        }

        /// <summary>
        /// Define execution method for each platform
        /// </summary>
        /// <returns></returns>
        private Dictionary<Platform, Func<IApplicationCollector>> GetExecutionMethods()
        {
            if (ExecutionMethods != null)
                return ExecutionMethods;

            ExecutionMethods = new Dictionary<Platform, Func<IApplicationCollector>>
            {
                { Platform.Win,  GetWindowsApplicationCollector },
                { Platform.Unix,  GetLinuxApplicationCollector }
            };

            return ExecutionMethods;
        }

        public IApplicationCollector GetApplicationCollector()
        {
            return _platform.PlatformInformation.ExecuteFunWithReturnValue(GetExecutionMethods());
        }

        private IApplicationCollector GetLinuxApplicationCollector()
        {
            throw new NotImplementedException();
        }

        private IApplicationCollector GetWindowsApplicationCollector()
        {
            return new DllUsageApplicationCollector(_logger);
        }
    }
}
