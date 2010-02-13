using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdamDotCom.OpenSource.Service.Proxy
{
    [ServiceContract]
    public interface IOpenSource
    {
        [OperationContract]
        [WebGet(UriTemplate = "projects/{projectHost}/xml?user={username}")]
        Projects GetProjectsByUsernameXml(string projectHost, string username);

        [OperationContract]
        [WebGet(UriTemplate = "projects/{projectHost}/json?user={username}", ResponseFormat = WebMessageFormat.Json)]
        Projects GetProjectsByUsernameJson(string projectHost, string username);

        [OperationContract]
        [WebGet(UriTemplate = "projects/xml?project-host:username={projectHostUsernamePair}")]
        Projects GetProjectsByProjectHostAndUsernameXml(string projectHostUsernamePair);

        [OperationContract]
        [WebGet(UriTemplate = "projects/json?project-host:username={projectHostUsernamePair}", ResponseFormat = WebMessageFormat.Json)]
        Projects GetProjectsByProjectHostAndUsernameJson(string projectHostUsernamePair);
    }
}