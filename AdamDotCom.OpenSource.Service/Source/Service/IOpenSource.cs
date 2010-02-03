using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

[assembly: ContractNamespace("http://adam.kahtava.com/services/opensource", ClrNamespace = "AdamDotCom.OpenSource.Service")]

namespace AdamDotCom.OpenSource.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/opensource")]
    public interface IOpenSource
    {
        [OperationContract]
        [WebGet(UriTemplate = "projects/{projectHost}/xml?user={username}")]
        Projects GetProjectsByUsernameXml(string projectHost, string username);

        [OperationContract]
        [WebGet(UriTemplate = "projects/{projectHost}/json?user={username}", ResponseFormat = WebMessageFormat.Json)]
        Projects GetProjectsByUsernameJson(string projectHost, string username);
    }
}