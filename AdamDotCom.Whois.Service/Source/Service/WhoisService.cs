using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AdamDotCom.Whois.Service.Utilities;
using AdamDotCom.Whois.Service.WhoisClient;

namespace AdamDotCom.Whois.Service
{
    public class WhoisService : IWhois
    {

        public WhoisRecord WhoisXml(string ipAddress)
        {
            return Whois(ipAddress);
        }

        public WhoisRecord WhoisJson(string ipAddress)
        {
            return Whois(ipAddress);
        }

        public WhoisEnhancedRecord WhoisEnhancedXml(string ipAddress, string filters, string referrer)
        {
            return WhoisEnhanced(ipAddress, filters, referrer);
        }

        public WhoisEnhancedRecord WhoisEnhancedJson(string ipAddress, string filters, string referrer)
        {
            return WhoisEnhanced(ipAddress, filters, referrer);
        }

        private WhoisRecord Whois(string ipAddress)
        {
            ipAddress = Scrub(ipAddress);
            if(string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = ((RemoteEndpointMessageProperty)OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
            }
            var whoisClient = new WhoisClient.WhoisClient(ipAddress);

            var record = whoisClient.GetWhoisRecord();

            HandleErrors(whoisClient.Errors);

            return record;
        }

        private WhoisEnhancedRecord WhoisEnhanced(string ipAddressOrDomainName, string filters, string referrer)
        {
            filters = Scrub(filters);
            referrer = Scrub(referrer);

            var whoisRecord = Whois(ipAddressOrDomainName);

            return new WhoisEnhancedRecord(whoisRecord, filters, referrer);
        }

        private static string Scrub(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value.Replace(" ", "%20").Replace("-", " ");
        }

        private static void HandleErrors(List<KeyValuePair<string, string>> errors)
        {
            if(errors != null && errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, errors, (int)ErrorCode.InternalError);
            }
        }
    }
}