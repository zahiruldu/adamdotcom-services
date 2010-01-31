using System;
using System.Configuration;
using System.Web;
using System.Web.Caching;

namespace AdamDotCom.Common.Service
{
    public static class ServiceCache
    {
        private static Cache cache;
        private static readonly bool enableCache;

        static ServiceCache()
        {   
            cache = HttpRuntime.Cache;
            enableCache = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableCaching"]);
        }

        private static bool IsInCache(string key)
        {
            return GetFromCache(key) != null;
        }

        private static object GetFromCache(string key)
        {
            if (cache[key] != null)
            {
                return cache[key];
            }
            return null;
        }

        private static void AddToCache(string key, object cacheObject)
        {
            if (enableCache)
            {
                cache.Insert(key, cacheObject, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
            }
        }

        public static T AddToCache<T>(this T value, string key)
        {
            AddToCache(Hash<T>(key), value);
            return value;
        }

        public static bool IsInCache<T>(string key)
        {
            return IsInCache(Hash<T>(key));
        }

        public static object GetFromCache<T>(string key)
        {
            return GetFromCache(Hash<T>(key));
        }

        private static string Hash<T>(string key)
        {
            return string.Format("{0}-{1}", typeof(T).GetType().FullName, key.ToLower());
        }
    }
}