using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using AdamDotCom.Common.Service.Infrastructure.CSV;
using AdamDotCom.Common.Service.Infrastructure.JSONP;

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
        [JSONP(callback = "callback"),
         WebGet(UriTemplate = "/json?query={ipAddress}", ResponseFormat = WebMessageFormat.Json)]
        WhoisRecord WhoisJson(string ipAddress);

        [OperationContract]
        [CSV, WebGet(UriTemplate = "/csv?query={ipAddress}")]
        WhoisRecord WhoisCsv(string ipAddress);

        [OperationContract]
        [WebGet(UriTemplate = "enhanced/xml?query={ipAddress}&filters={filters}&referrer={referrer}")]
        WhoisEnhancedRecord WhoisEnhancedXml(string ipAddress, string filters, string referrer);

        [OperationContract]
        [JSONP(callback = "callback"),
         WebGet(UriTemplate = "enhanced/json?query={ipAddress}&filters={filters}&referrer={referrer}", ResponseFormat = WebMessageFormat.Json)]
        WhoisEnhancedRecord WhoisEnhancedJson(string ipAddress, string filters, string referrer);

        [OperationContract]
        [CSV, WebGet(UriTemplate = "enhanced/csv?query={ipAddress}&filters={filters}&referrer={referrer}")]
        WhoisEnhancedRecord WhoisEnhancedCsv(string ipAddress, string filters, string referrer);
   }
}