using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Interfaces;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProcessExplorer.Service.Process
{
    public class PlatformProcessRecognizer : IPlatformProcessRecognizer
    {
        private static Dictionary<Platform, Func<string, bool>> ExecutionMethods;

        private readonly IPlatformInformationService _platform;
        private readonly PlatformRecognizerOptions _options;

        public PlatformProcessRecognizer(IPlatformInformationService platform,
            IOptions<PlatformRecognizerOptions> options)
        {
            _platform = platform;
            _options = options.Value;
        }

        private Dictionary<Platform, Func<string, bool>> GetExecutionMethods()
        {
            if (ExecutionMethods != null)
                return ExecutionMethods;

            ExecutionMethods = new Dictionary<Platform, Func<string, bool>>
            {
                { Platform.Win,  IsWindowsPath },
                { Platform.Unix,  IsLinuxPath }
            };

            return ExecutionMethods;
        }
        
        public bool IsPlatfromProcess(string processPath)
        {
            if (!_options.UseRegex)
                return false;

            if (processPath == null)
                throw new ArgumentNullException(nameof(processPath));

            return _platform.PlatformInformation.ExecuteFunWithReturnValue(GetExecutionMethods(), processPath);
        }

        public bool IsWindowsPath(string processPath)
        {
            if (RegexManager.ValidRegex(nameof(_options.WindowsRegex), processPath))
                return true;
            return false;
        }

        public bool IsLinuxPath(string processPath)
        {
            return false;
        }
    }
}
