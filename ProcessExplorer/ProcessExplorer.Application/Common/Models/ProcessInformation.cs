using System.Linq;

namespace ProcessExplorer.Application.Common.Models
{
    public class ProcessInformation
    {
        public string ProcessTitle { get; set; }
        public string PrettyProccessTitle 
        {
            get
            {
                if (string.IsNullOrEmpty(ProcessTitle))
                    return null;

                return ProcessTitle.Split("-").Select(i => i?.Trim())?.Last();
            }
        }
        public bool IsVisibleApplication => string.IsNullOrEmpty(ProcessTitle) ? false : true;
        public string ProcessName { get; set; }
        public string ProcessNameNormalized => ProcessName.ToUpper();
        public string ProcessPath { get; set; }
    }
}
