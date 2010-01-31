using System.Collections.Generic;
using AdamDotCom.Common.Service;

namespace AdamDotCom.OpenSource.Service
{
    public static class CacheExtensions
    {
        public static List<Project> GetFromCache(this List<Project> projects, string host, string username)
        {
            var key = GetKey(host, username);
            if (GetFromCache<List<Project>>(key) != default(List<Project>))
            {
                return GetFromCache<List<Project>>(key);
            }
            return null;
        }

        public static List<Project> AddToCache(this List<Project> projects, string host, string username)
        {
            return projects.AddToCache(GetKey(host, username));
        }

        private static T GetFromCache<T>(string key)
        {
            if (ServiceCache.IsInCache<T>(key))
            {
                return (T) ServiceCache.GetFromCache<T>(key);
            }
            return default(T);
        }

        private static string GetKey(string host, string username)
        {
            return string.Format("{0}-{1}", host, username);
        }

    }
}