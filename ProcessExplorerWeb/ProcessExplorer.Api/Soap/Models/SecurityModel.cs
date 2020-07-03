using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Soap.Models
{
    [DataContract]
    public abstract class SecurityModel
    {
        [DataMember]
        public string Jwt { get; set; }
    }
}
