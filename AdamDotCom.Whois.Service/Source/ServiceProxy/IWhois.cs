using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdamDotCom.Whois.Service.Proxy
{
    [ServiceContract]
    public interface IWhois
    {
        [OperationContract]
        [WebGet(UriTemplate = "/xml?query={ipAddress}")]
        WhoisRecord WhoisXml(string ipAddress);

        [OperationContract]
        [WebGet(UriTemplate = "/json?query={ipAddress}", ResponseFormat = WebMessageFormat.Json)]
        WhoisRecord WhoisJson(string ipAddress);

        [OperationContract]
        [WebGet(UriTemplate = "enhanced/xml?query={ipAddress}&filters={filters}&referrer={referrer}")]
        WhoisEnhancedRecord WhoisEnhancedXml(string ipAddress, string filters, string referrer);

        [OperationContract]
        [WebGet(UriTemplate = "enhanced/json?query={ipAddress}&filters={filters}&referrer={referrer}", ResponseFormat = WebMessageFormat.Json)]
        WhoisEnhancedRecord WhoisEnhancedJson(string ipAddress, string filters, string referrer);
    }
}
