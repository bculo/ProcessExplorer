using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Soap.Models
{
    [DataContract]
    public class SyncProcessModel : SecurityModel
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string OS { get; set; }
        [DataMember]
        public DateTime Started { get; set; }
        [DataMember]
        public List<ProcessInstanceModel> Processes { get; set; }
    }
}
