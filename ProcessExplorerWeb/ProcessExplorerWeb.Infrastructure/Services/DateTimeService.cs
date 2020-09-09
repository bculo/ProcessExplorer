using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public TimeZoneInfo EST { get; set; } 
            = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public DateTime Now => TimeZoneInfo.ConvertTime(DateTime.Now, EST);
    }
}
