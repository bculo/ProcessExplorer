using ProcessExplorer.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessExplorer.Service.Options
{
    public class ProcessCollectorUsageOptions
    {
        public List<FactoryOption> Options { get; set; }

        public string GetActiveOptionFor(Platform platform)
        {
            var searchResult = Options.FirstOrDefault(i => i.Platform == platform && i.Use);

            if (searchResult == null)
                return string.Empty;

            return searchResult.Name;
        }
    }
}
