using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Resume.Service.Proxy
{
    [DataContract]
    public class Resume
    {
        [DataMember]
        public List<Education> Educations;

        [DataMember]
        public List<Position> Positions;

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public string Specialties { get; set; }
    }

    [DataContract]
    public class Position
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Company { get; set; }

        [DataMember]
        public string Period { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class Education
    {
        [DataMember]
        public string Institute { get; set; }

        [DataMember]
        public string Certificate { get; set; }
    }
}