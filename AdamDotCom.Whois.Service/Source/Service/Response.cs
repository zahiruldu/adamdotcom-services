using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Whois.Service
{
    [DataContract]
    public class Response : WhoisRecord
    {
        public string City { get; set; }
        public string Region { get; set; }
        public string Organization { get; set; }

        public string CountryCode2 { get; set; }

        [DataMember]
        public bool IsFriendly { get; set; }

        [DataMember]
        public bool IsFilterMatch { get; set; }

        [DataMember]
        public List<string> FilterMatches { get; set; }

        [DataMember]
        public List<string> FriendlyMatches { get; set; }

        public string Country { get; set; }
    }
}