using System.Runtime.Serialization;
using System.ServiceModel;

[assembly: ContractNamespace("http://adam.kahtava.com/services/open-source", ClrNamespace = "AdamDotCom.OpenSource.Service.Proxy")]

namespace AdamDotCom.OpenSource.Service.Proxy
{
    public class OpenSourceService : ClientBase<IOpenSource>, IOpenSource
    {
        public Projects GetProjectsByUsernameXml(string projectHost, string username)
        {
            return base.Channel.GetProjectsByUsernameXml(projectHost, username);
        }

        public Projects GetProjectsByUsernameJson(string projectHost, string username)
        {
            return base.Channel.GetProjectsByUsernameJson(projectHost, username);
        }

        public Projects GetProjectsByProjectHostAndUsernameXml(string projectHostUsernamePair, string filters)
        {
            return base.Channel.GetProjectsByProjectHostAndUsernameXml(projectHostUsernamePair, filters);
        }

        public Projects GetProjectsByProjectHostAndUsernameJson(string projectHostUsernamePair, string filters)
        {
            return base.Channel.GetProjectsByProjectHostAndUsernameJson(projectHostUsernamePair, filters);
        }
    }
}