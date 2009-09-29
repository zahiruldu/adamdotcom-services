using System.Runtime.Serialization;
using System.ServiceModel;

[assembly: ContractNamespace("http://adam.kahtava.com/services/whois", ClrNamespace = "AdamDotCom.Whois.Service.Proxy")]
namespace AdamDotCom.Whois.Service.Proxy
{
    public class WhoisService : ClientBase<IWhois>, IWhois
    {
        public WhoisRecord WhoisXml(string ipAddress)
        {
            return base.Channel.WhoisXml(ipAddress);
        }

        public WhoisRecord WhoisJson(string ipAddress)
        {
            return base.Channel.WhoisJson(ipAddress);
        }

        public WhoisEnhancedRecord WhoisEnhancedXml(string ipAddress, string filters, string referrer)
        {
            return base.Channel.WhoisEnhancedXml(ipAddress, filters, referrer);
        }

        public WhoisEnhancedRecord WhoisEnhancedJson(string ipAddress, string filters, string referrer)
        {
            return base.Channel.WhoisEnhancedJson(ipAddress, filters, referrer);
        }
    }
}