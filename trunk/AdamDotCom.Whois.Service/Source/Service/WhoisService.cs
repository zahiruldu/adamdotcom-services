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
        public WhoisRecord WhoAmIXml()
        {
            return Whois(null);
        }

        public WhoisRecord WhoAmIJson()
        {
            return Whois(null);
        }

        public WhoisRecord WhoisXml(string ipOrDomain)
        {
            return Whois(ipOrDomain);
        }

        public WhoisRecord WhoisJson(string ipOrDomain)
        {
            return Whois(ipOrDomain);
        }

        public WhoisEnhancedRecord WhoisEnhancedXml(string filters)
        {
            return WhoisEnhanced(filters, null);
        }

        public WhoisEnhancedRecord WhoisEnhancedJson(string filters)
        {
            return WhoisEnhanced(filters, null);
        }

        public WhoisEnhancedRecord WhoisEnhancedWithReferrerXml(string filters, string referrer)
        {
            return WhoisEnhanced(filters, referrer);
        }

        public WhoisEnhancedRecord WhoisEnhancedWithReferrerJson(string filters, string referrer)
        {
            return WhoisEnhanced(filters, referrer);
        } 
        private WhoisRecord Whois(string ipOrDomain)
        {
            ipOrDomain = Scrub(ipOrDomain);
            if(string.IsNullOrEmpty(ipOrDomain))
            {
                ipOrDomain = ((RemoteEndpointMessageProperty)OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
            }
            var whoisServiceTranslator = new WhoisClient.WhoisClient(ipOrDomain);

            var record = whoisServiceTranslator.GetWhoisRecord();

            if (record == null)
            {
                throw new RestException(string.Format("{0} could not be found", ipOrDomain));
            }

            return record;
        }

        private WhoisEnhancedRecord WhoisEnhanced(string filters, string referrer)
        {
            filters = Scrub(filters);
            referrer = Scrub(referrer);

            var whoisRecord = Whois(null);

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