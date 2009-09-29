using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Whois.Service.Proxy
{
    [DataContract]
    public class WhoisEnhancedRecord
    {
        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string StateProvince { get; set; }
        
        [DataMember]
        public string Organization { get; set; }

        [DataMember]
        public bool IsFriendly { get; set; }

        [DataMember]
        public bool IsFilterMatch { get; set; }

        [DataMember]
        public List<string> FilterMatches { get; set; }

        [DataMember]
        public List<string> FriendlyMatches { get; set; }

        [DataMember]
        public string Country { get; set; }
    }
}