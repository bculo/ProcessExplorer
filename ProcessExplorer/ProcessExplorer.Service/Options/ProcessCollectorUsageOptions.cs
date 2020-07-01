using ProcessExplorer.Core.Enums;
using System.Collections.Generic;
using System.Linq;

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
