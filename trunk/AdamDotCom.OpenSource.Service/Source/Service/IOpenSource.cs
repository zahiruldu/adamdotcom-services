using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

[assembly: ContractNamespace("http://adam.kahtava.com/services/open-source", ClrNamespace = "AdamDotCom.OpenSource.Service")]

namespace AdamDotCom.OpenSource.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/open-source")]
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

        [OperationContract]
        [WebGet(UriTemplate = "projects/html?project-host:username={projectHostUsernamePair}")]
        Stream GetProjectsByProjectHostAndUsernameHtml(string projectHostUsernamePair);
    }
}