using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AdamDotCom.Common.Service.Infrastructure;
using AdamDotCom.Whois.Service.Extensions;
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
            AssertValidInput(ipAddress, "ipAddress");

            if (ServiceCache.IsInCache(ipAddress))
            {
                var cachedRecord = (WhoisRecord) ServiceCache.GetFromCache(ipAddress);
                if (cachedRecord != null)
                {
                    return cachedRecord;
                }
            }

            var whoisClient = new WhoisClient.WhoisClient(ipAddress);

            var record = whoisClient.GetWhoisRecord();

            HandleErrors(whoisClient.Errors);

            return record.AddToCache(ipAddress);
        }

        private WhoisEnhancedRecord WhoisEnhanced(string ipAddress, string filters, string referrer)
        {
            filters = Scrub(filters);
            referrer = Scrub(referrer);
            AssertValidInput(filters, "filters");
            AssertValidInput(referrer, "referrer");

            var hash = string.Format("{0}-{1}", ipAddress, filters).ToLower().Replace(",", "-").Replace(" ", "-");
            if (ServiceCache.IsInCache(hash))
            {
                var cachedRecord = (WhoisEnhancedRecord) ServiceCache.GetFromCache(hash);
                if (cachedRecord != null)
                {
                    return cachedRecord;
                }
            }

            var whoisRecord = Whois(ipAddress);

            var whoisEnhancedRecord = new WhoisEnhancedRecord(whoisRecord, filters, referrer);

            return whoisEnhancedRecord.AddToCache(hash);
        }

        private static string Scrub(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value.Replace("%20", " ").Replace("-", " ");
        }

        private static void AssertValidInput(string inputValue, string inputName)
        {
            inputName = (string.IsNullOrEmpty(inputName) ? "Unknown" : inputName);

            if (!string.IsNullOrEmpty(inputValue) && (inputValue.Equals("null", StringComparison.CurrentCultureIgnoreCase) || inputValue.Equals("string.empty", StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new RestException(new KeyValuePair<string, string>(inputName, string.Format("{0} is not a valid parameter value.", inputValue)));
            }
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