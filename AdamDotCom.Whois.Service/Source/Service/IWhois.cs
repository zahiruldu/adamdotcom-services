using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

[assembly: ContractNamespace("http://adam.kahtava.com/services/whois", ClrNamespace = "AdamDotCom.Whois.Service")]
namespace AdamDotCom.Whois.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/whois")]
    public interface IWhois
    {
        [OperationContract]
        [WebGet(UriTemplate = "filters/{filters}/xml")]
        Response WhoisXml(string filters);

        [OperationContract]
        [WebGet(UriTemplate = "filters/{filters}/json", ResponseFormat = WebMessageFormat.Json)]
        Response WhoisJson(string filters);

        [OperationContract]
        [WebGet(UriTemplate = "filters/{filters}/referrer/{referrer}/xml")]
        Response WhoisWithReferrerXml(string filters, string referrer);

        [OperationContract]
        [WebGet(UriTemplate = "filters/{filters}/referrer/{referrer}/json", ResponseFormat = WebMessageFormat.Json)]
        Response WhoisWithReferrerJson(string filters, string referrer);

   }
}