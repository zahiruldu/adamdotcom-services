using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AdamDotCom.Common.Service;
using AdamDotCom.Common.Service.Infrastructure;
using AdamDotCom.Common.Service.Utilities;

namespace AdamDotCom.Whois.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
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
            ipAddress = ipAddress.Scrub();
            ipAddress = GetIpAddress(ipAddress);
            Assert.ValidInput(ipAddress, "ipAddress");

            if (ServiceCache.IsInCache<WhoisRecord>(ipAddress))
            {
                var cachedRecord = (WhoisRecord) ServiceCache.GetFromCache<WhoisRecord>(ipAddress);
                if (cachedRecord != null)
                {
                    return cachedRecord;
                }
            }

            var whoisClient = new WhoisClient(ipAddress);
            var record = whoisClient.GetWhoisRecord();

            HandleErrors(whoisClient.Errors);

            return record.AddToCache(ipAddress);
        }

        private WhoisEnhancedRecord WhoisEnhanced(string ipAddress, string filters, string referrer)
        {
            if (filters != null)
            {
                filters = filters.Scrub();
                Assert.ValidInput(filters, "filters");
            }
            if (referrer != null)
            {
                referrer = referrer.Scrub();
                Assert.ValidInput(referrer, "referrer");
            }

            var hash = BuildHash(ipAddress, filters);

            if (ServiceCache.IsInCache<WhoisEnhancedRecord>(hash))
            {
                var cachedRecord = (WhoisEnhancedRecord)ServiceCache.GetFromCache<WhoisEnhancedRecord>(hash);
                if (cachedRecord != null)
                {
                    return cachedRecord;
                }
            }

            var whoisEnhancedRecord = new WhoisEnhancedRecord(Whois(ipAddress), filters, referrer);

            return whoisEnhancedRecord.AddToCache(hash);
        }

        private static string GetIpAddress(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = ((RemoteEndpointMessageProperty)OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
            }
            return ipAddress;
        }

        private static void HandleErrors(List<KeyValuePair<string, string>> errors)
        {
            if(errors != null && errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, errors, (int)ErrorCode.InternalError);
            }
        }

        private static string BuildHash(string ipAddress, string filters)
        {
            return string.Format("{0}-{1}", ipAddress, filters).ToLower().Replace(",", "-").Replace(" ", "-");           
        }
    }
}