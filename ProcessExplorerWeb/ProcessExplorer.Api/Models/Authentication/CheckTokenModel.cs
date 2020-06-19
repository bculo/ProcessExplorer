using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Models.Authentication
{
    public class CheckTokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}
