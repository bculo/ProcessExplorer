﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Session.SharedDtos
{
    public class SessionMostActiveDayDto
    {
        public DateTime Date { get; set; }
        public int NumberOfSessions { get; set; }
    }
}
