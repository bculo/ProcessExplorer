using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Options;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Interfaces;
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

        public ProcessCollectorFactory(IPlatformInformationService platform,
            IPlatformProcessRecognizer recognizer)
        {
            _platform = platform;
            _recognizer = recognizer;
        }

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

        public IProcessCollector GetProcessCollector()
        {
            return _platform.PlatformInformation.ExecuteFunWithReturnValue(GetExecutionMethods());
        }

        private IProcessCollector GetWindowsCollector()
        {
            return new WMIProcessCollector(_recognizer);
        }

        private IProcessCollector GetLinuxCollector()
        {
            return new AllProcessCollector(_recognizer);
        }
    }
}
