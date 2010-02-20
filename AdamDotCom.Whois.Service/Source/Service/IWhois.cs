using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using AdamDotCom.Common.Service.Infrastructure.JSONP;
using AdamDotCom.Whois.Service.WhoisClient;

[assembly: ContractNamespace("http://adam.kahtava.com/services/whois", ClrNamespace = "AdamDotCom.Whois.Service")]
namespace AdamDotCom.Whois.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/whois")]
    public interface IWhois
    {
        [OperationContract]
        [WebGet(UriTemplate = "/xml?query={ipAddress}")]
        WhoisRecord WhoisXml(string ipAddress);

        [OperationContract]
        [JSONPBehavior(callback = "callback")]
        [WebGet(UriTemplate = "/json?query={ipAddress}", ResponseFormat = WebMessageFormat.Json)]
        WhoisRecord WhoisJson(string ipAddress);
        
        [OperationContract]
        [WebGet(UriTemplate = "enhanced/xml?query={ipAddress}&filters={filters}&referrer={referrer}")]
        WhoisEnhancedRecord WhoisEnhancedXml(string ipAddress, string filters, string referrer);

        [OperationContract]
        [JSONPBehavior(callback = "callback")]
        [WebGet(UriTemplate = "enhanced/json?query={ipAddress}&filters={filters}&referrer={referrer}", ResponseFormat = WebMessageFormat.Json)]
        WhoisEnhancedRecord WhoisEnhancedJson(string ipAddress, string filters, string referrer);
   }
}