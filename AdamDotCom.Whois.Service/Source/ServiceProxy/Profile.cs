using System.Runtime.Serialization;

namespace AdamDotCom.Whois.Service.Proxy
{
    [DataContract]
    public class Profile
    {
        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string ListId { get; set; }
    }
}