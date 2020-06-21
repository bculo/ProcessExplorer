using System;
using ProcessExplorer.Application.Common.Interfaces;

namespace ProcessExplorer.Service.Time
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}