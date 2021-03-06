﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Models
{
    public class ApplicationInformation
    {
        public DateTime StartTime { get; set; }
        public string ApplicationName { get; set; }
        public Guid Session { get; set; }
        public DateTime FetchTime { get; set; }
    }
}
