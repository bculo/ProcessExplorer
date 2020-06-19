using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Models.Authentication
{
    public class LoginResponseModel
    {
        public Guid UserId { get; set; }
        public string JwtToken { get; set; }
    }
}
