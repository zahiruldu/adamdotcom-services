using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using AdamDotCom.Whois.Service.WhoisClient;

[assembly: ContractNamespace("http://adam.kahtava.com/services/whois", ClrNamespace = "AdamDotCom.Whois.Service")]
namespace AdamDotCom.Whois.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/whois")]
    public interface IWhois
    {
        [OperationContract]
        [WebGet(UriTemplate = "/xml")]
        WhoisRecord WhoAmIXml();

        [OperationContract]
        [WebGet(UriTemplate = "/json", ResponseFormat = WebMessageFormat.Json)]
        WhoisRecord WhoAmIJson();

        [OperationContract]
        [WebGet(UriTemplate = "{ipOrDomain}/xml")]
        WhoisRecord WhoisXml(string ipOrDomain);

        [OperationContract]
        [WebGet(UriTemplate = "{ipOrDomain}/json", ResponseFormat = WebMessageFormat.Json)]
        WhoisRecord WhoisJson(string ipOrDomain);

        [OperationContract]
        [WebGet(UriTemplate = "enhanced/filters/{filters}/xml")]
        WhoisEnhancedRecord WhoisEnhancedXml(string filters);

        [OperationContract]
        [WebGet(UriTemplate = "enhanced/filters/{filters}/json", ResponseFormat = WebMessageFormat.Json)]
        WhoisEnhancedRecord WhoisEnhancedJson(string filters);

        [OperationContract]
        [WebGet(UriTemplate = "enhanced/filters/{filters}/referrer/{referrer}/xml")]
        WhoisEnhancedRecord WhoisEnhancedWithReferrerXml(string filters, string referrer);

        [OperationContract]
        [WebGet(UriTemplate = "enhanced/filters/{filters}/referrer/{referrer}/json", ResponseFormat = WebMessageFormat.Json)]
        WhoisEnhancedRecord WhoisEnhancedWithReferrerJson(string filters, string referrer);
   }
}