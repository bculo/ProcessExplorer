using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;

namespace ProcessExplorer.Application.Utils
{
    public static class PlatformInformationExtension
    {
        public static Result ExecuteFunWithReturnValue<Result>(this PlatformInformation information, Dictionary<Platform, Func<Result>> dictionary)
        {
            switch (information.Type)
            {
                case Platform.Win:
                    return dictionary[Platform.Win]();
                case Platform.Unix:
                    return dictionary[Platform.Unix]();
                default:
                    throw new NotSupportedException("Platform not supported");
            }
        }

        public static Result ExecuteFunWithReturnValue<T, Result>(this PlatformInformation information, Dictionary<Platform, Func<T, Result>> dictionary, T arg)
        {
            switch (information.Type)
            {
                case Platform.Win:
                    return dictionary[Platform.Win](arg);
                case Platform.Unix:
                    return dictionary[Platform.Unix](arg);
                default:
                    throw new NotSupportedException("Platform not supported");
            }
        }
    }
}
