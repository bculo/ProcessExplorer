using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using HWND = System.IntPtr;

namespace ProcessExplorer.Service.Application.Windows
{
    public class DllUsageApplicationCollector : RootAppCollector, IApplicationCollector
    {
        public DllUsageApplicationCollector(ILoggerWrapper logger, ISessionService sessionService, IDateTime time) 
            : base(logger, sessionService, time)
        {
        }

        public List<ApplicationInformation> GetApplications()
        {
            _logger.LogInfo($"Started fetching applications in {nameof(DllUsageApplicationCollector)}");

            var appList = new List<ApplicationInformation>();
            foreach (KeyValuePair<IntPtr, string> window in GetOpenWindows())
            {
                GetWindowThreadProcessId(window.Key, out uint processId);
                var process = System.Diagnostics.Process.GetProcessById((int) processId);
                appList.Add(new ApplicationInformation
                {
                    StartTime = process.StartTime,
                    ApplicationName = GetBasicApplicationTitle(window.Value),
                    Session = _sessionService.SessionInformation.SessionId,
                    FetchTime = _dateTime.Now
                });
            }

            _logger.LogInfo($"Application fetched in {nameof(DllUsageApplicationCollector)} : {appList.Count}");
            return appList;
        }

        public IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        [DllImport("USER32.DLL")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }
}
