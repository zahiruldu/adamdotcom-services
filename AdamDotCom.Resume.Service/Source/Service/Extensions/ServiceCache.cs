using System;
using System.Web;
using System.Web.Caching;

namespace AdamDotCom.Resume.Service.Extensions
{
    public static class ServiceCache
    {
        private static Cache cache = HttpRuntime.Cache;

        public static bool IsInCache(string key)
        {
            return GetFromCache(key) != null;
        }

        public static object GetFromCache(string key)
        {
            if (cache[key] != null)
            {
                return cache[key];
            }
            return null;
        }

        public static Resume AddToCache(this Resume resume, string username)
        {
            AddToCache(username, resume);
            
            return resume;
        }

        private static void AddToCache(string key, object cacheObject)
        {
            cache.Insert(key, cacheObject, null, DateTime.Now.AddDays(1d), Cache.NoSlidingExpiration);
        }
    }
}
