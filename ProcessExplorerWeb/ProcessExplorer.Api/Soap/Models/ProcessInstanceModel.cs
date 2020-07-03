using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProcessExplorer.Api.Soap.Models
{
    [DataContract]
    public class ProcessInstanceModel
    {
        [MessageHeader]
        public string JWT { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime Detected { get; set; }
        [DataMember]
        public string SessionId { get; set; }
    }
}
