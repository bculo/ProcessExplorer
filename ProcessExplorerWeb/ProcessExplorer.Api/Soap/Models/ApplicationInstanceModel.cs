using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Soap.Models
{
    [DataContract]
    public class ApplicationInstanceModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime Started { get; set; }
        [DataMember]
        public DateTime LastUse { get; set; }
        [DataMember]
        public string SessionId { get; set; }
    }
}
