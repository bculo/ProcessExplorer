using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Options;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Interfaces;
using ProcessExplorer.Service.Options;
using ProcessExplorer.Service.Process.Linux;
using ProcessExplorer.Service.Process.Windows;
using System;
using System.Collections.Generic;

namespace ProcessExplorer.Service.Process
{
    public class ProcessCollectorFactory : IProcessCollectorFactory
    {
        private static Dictionary<Platform, Func<IProcessCollector>> ExecutionMethods;

        private readonly IPlatformInformationService _platform;
        private readonly IPlatformProcessRecognizer _recognizer;
        private readonly ILoggerWrapper _logger;
        private readonly IOptions<ProcessCollectorOptions> _options;
        private readonly ProcessCollectorUsageOptions _usageOptions;

        public ProcessCollectorFactory(IPlatformInformationService platform,
            IPlatformProcessRecognizer recognizer,
            ILoggerWrapper logger,
            IOptions<ProcessCollectorOptions> options,
            IOptions<ProcessCollectorUsageOptions> usageOptions)
        {
            _platform = platform;
            _recognizer = recognizer;
            _logger = logger;
            _options = options;
            _usageOptions = usageOptions.Value;
        }

        /// <summary>
        /// Define execution method for each platform
        /// </summary>
        /// <returns></returns>
        private Dictionary<Platform, Func<IProcessCollector>> GetExecutionMethods()
        {
            if (ExecutionMethods != null)
                return ExecutionMethods;

            ExecutionMethods = new Dictionary<Platform, Func<IProcessCollector>>
            {
                { Platform.Win,  GetWindowsCollector },
                { Platform.Unix,  GetLinuxCollector }
            };

            return ExecutionMethods;
        }

        /// <summary>
        /// Get process collector based on platform and settings
        /// </summary>
        /// <returns></returns>
        public IProcessCollector GetProcessCollector()
        {
            return _platform.PlatformInformation.ExecuteFunWithReturnValue(GetExecutionMethods());
        }

        /// <summary>
        /// Get Collector for windows
        /// </summary>
        /// <returns></returns>
        private IProcessCollector GetWindowsCollector()
        {
            var result = _usageOptions.GetActiveOptionFor(Platform.Win);

            switch (result)
            {
                case nameof(WMIProcessCollector):
                    return new WMIProcessCollector(_recognizer, _logger, _options);
                case nameof(Kernel32ProcessCollector):
                    return new Kernel32ProcessCollector(_recognizer, _logger, _options);
                case nameof(AllProcessCollector):
                    return new Kernel32ProcessCollector(_recognizer, _logger, _options);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Get Collector for Linux
        /// </summary>
        /// <returns></returns>
        private IProcessCollector GetLinuxCollector()
        {
            var result = _usageOptions.GetActiveOptionFor(Platform.Unix);

            switch(result)
            {
                case nameof(AllProcessCollector):
                    return new AllProcessCollector(_recognizer, _logger, _options);
                case nameof(PsAuxProcessCollector):
                    return new PsAuxProcessCollector(_logger, _platform, _recognizer, _options);
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
