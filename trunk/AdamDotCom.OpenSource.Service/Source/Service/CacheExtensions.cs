using System.Collections.Generic;
using AdamDotCom.Common.Service;

namespace AdamDotCom.OpenSource.Service
{
    public static class CacheExtensions
    {
        public static List<Project> GetFromCache(this List<Project> projects, ProjectHost projectHost, string username)
        {
            var key = GetKey(projectHost, username);
            if (GetFromCache<List<Project>>(key) != default(List<Project>))
            {
                return GetFromCache<List<Project>>(key);
            }
            return null;
        }

        public static List<Project> AddToCache(this List<Project> projects, ProjectHost projectHost, string username)
        {
            return projects.AddToCache(GetKey(projectHost, username));
        }

        private static T GetFromCache<T>(string key)
        {
            if (ServiceCache.IsInCache<T>(key))
            {
                return (T) ServiceCache.GetFromCache<T>(key);
            }
            return default(T);
        }

        private static string GetKey(ProjectHost projectHost, string username)
        {
            return string.Format("{0}-{1}", projectHost, username);
        }

    }
}