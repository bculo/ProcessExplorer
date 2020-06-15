using ProcessExplorer.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Service.Options
{
    public class FactoryOption
    {
        public Platform Platform { get; set; }
        public bool Use { get; set; }
        public string Name { get; set; }
    }
}
