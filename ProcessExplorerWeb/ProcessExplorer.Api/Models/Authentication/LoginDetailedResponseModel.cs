using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Models.Authentication
{
    public class LoginDetailedResponseModel : LoginResponseModel
    {
        public string UserName { get; set; }
        public long ExpireIn { get; set; }
    }
}
