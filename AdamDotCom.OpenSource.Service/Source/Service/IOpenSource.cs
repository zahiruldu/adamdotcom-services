using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using AdamDotCom.Common.Service.Infrastructure.JSONP;

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
        [JSONPBehavior(callback = "callback")]
        [WebGet(UriTemplate = "projects/{projectHost}/json?user={username}", ResponseFormat = WebMessageFormat.Json)]
        Projects GetProjectsByUsernameJson(string projectHost, string username);

        [OperationContract]
        [WebGet(UriTemplate = "projects/xml?project-host:username={projectHostUsernamePair}&filters={filters}")]
        Projects GetProjectsByProjectHostAndUsernameXml(string projectHostUsernamePair, string filters);

        [OperationContract]
        [JSONPBehavior(callback = "callback")]
        [WebGet(UriTemplate = "projects/json?project-host:username={projectHostUsernamePair}&filters={filters}", ResponseFormat = WebMessageFormat.Json)]
        Projects GetProjectsByProjectHostAndUsernameJson(string projectHostUsernamePair, string filters);
    }
}