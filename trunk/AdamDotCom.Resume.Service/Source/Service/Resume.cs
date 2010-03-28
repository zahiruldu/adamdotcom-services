using System.Collections.Generic;

namespace AdamDotCom.Resume.Service
{
    public class Resume
    {
        public List<Education> Educations;
        public List<Position> Positions;
        public string Summary { get; set; }
        public string Specialties { get; set; }
    }

    public class Position
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Period { get; set; }
        public string Description { get; set; }
    }

    public class Education
    {
        public string Institute { get; set; }
        public string Certificate { get; set; }
        public string Period { get; set; }
    }
}