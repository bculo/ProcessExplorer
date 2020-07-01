using System;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ILoggerWrapper
    {
        void LogInfo(string content, params object[] param);
        void LogError(Exception e);
    }
}
