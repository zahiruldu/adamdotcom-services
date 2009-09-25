using System.Runtime.Serialization;

namespace AdamDotCom.Whois.Service
{
    [DataContract]
    public class WhoisTranslatorResponse
    {
        [DataMember]
        public string Organization { get; set; }

        [DataMember]
        public string Country { get; set; }

        public string CountryCode2 { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public string City { get; set; }
    }
}
