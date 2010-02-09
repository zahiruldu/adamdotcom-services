using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.OpenSource.Service.Proxy
{
    [CollectionDataContract(Name = "Projects", ItemName = "Project")]
    public class Projects : List<Project>
    {
        public Projects()
        {
        }

        public Projects(IEnumerable<Project> project)
            : base(project)
        {
        }
    }
}