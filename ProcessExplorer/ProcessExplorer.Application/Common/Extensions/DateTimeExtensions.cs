using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertEstToCet(this DateTime fetchedTime)
        {
            return fetchedTime.AddHours(6);
        }
    }
}
