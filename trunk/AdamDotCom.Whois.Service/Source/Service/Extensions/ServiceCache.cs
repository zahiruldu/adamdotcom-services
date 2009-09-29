using AdamDotCom.Whois.Service.WhoisClient;
using AdamDotCom.Whois.Service;

namespace AdamDotCom.Whois.Service.Extensions
{
    public static class ServiceCache
    {
        public static WhoisRecord AddToCache(this WhoisRecord whoisRecord, string ipAddress)
        {
            Common.Service.ServiceCache.AddToCache(ipAddress, whoisRecord);

            return whoisRecord;
        }

        public static WhoisEnhancedRecord AddToCache(this WhoisEnhancedRecord whoisEnhancedRecord, string ipAddress)
        {
            Common.Service.ServiceCache.AddToCache(ipAddress, whoisEnhancedRecord);

            return whoisEnhancedRecord;
        }

        public static bool IsInCache(string key)
        {
            return Common.Service.ServiceCache.IsInCache(key);
        }

        public static object GetFromCache(string key)
        {
            return Common.Service.ServiceCache.GetFromCache(key);
        }
    }
}