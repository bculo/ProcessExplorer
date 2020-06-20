using System;
using System.ComponentModel.DataAnnotations;

namespace ProcessExplorer.Api.Models.Authentication
{
    public class SessionModel
    {
        [Required]
        public Guid SesssionId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public DateTime Started { get; set; }
    }
}
