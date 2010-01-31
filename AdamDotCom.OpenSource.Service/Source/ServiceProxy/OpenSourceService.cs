using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

[assembly: ContractNamespace("http://adam.kahtava.com/services/opensource", ClrNamespace = "AdamDotCom.OpenSource.Service.Proxy")]

namespace AdamDotCom.OpenSource.Service.Proxy
{
    public class OpenSourceService : ClientBase<IOpenSource>, IOpenSource
    {
        public List<Project> GetProjectsByUsernameXml(string projectHost, string username)
        {
            return base.Channel.GetProjectsByUsernameXml(projectHost, username);
        }

        public List<Project> GetProjectsByUsernameJson(string projectHost, string username)
        {
            return base.Channel.GetProjectsByUsernameJson(projectHost, username);
        }
    }
}