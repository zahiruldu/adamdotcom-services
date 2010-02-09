using System.Runtime.Serialization;

namespace AdamDotCom.OpenSource.Service.Proxy
{
    [DataContract]
    public class Project
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Url { get; set; }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string LastModified { get; set; }

        [DataMember]
        public string LastMessage { get; set; }
    }
}