using System;

namespace ProcessExplorer.Core.Entities
{
    public class Authentication : Entity<int>
    {
        /// <summary>
        /// JWT Token
        /// </summary>
        public string Content { get; set; }
        public DateTime Inserted { get; set; }
    }
}
