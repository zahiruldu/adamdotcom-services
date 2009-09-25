using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Whois.Service
{
    [DataContract]
    public class Response: WhoisTranslatorResponse
    {
        [DataMember]
        public bool IsFriendly { get; set; }

        [DataMember]
        public bool IsFilterMatch { get; set; }

        [DataMember]
        public List<string> FilterMatches { get; set; }

        [DataMember]
        public List<string> FriendlyMatches { get; set; }
    }
}