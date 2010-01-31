using System.Runtime.Serialization;

namespace AdamDotCom.OpenSource.Service
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
    }
}