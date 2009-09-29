namespace AdamDotCom.Resume.Service.Extensions
{
    public static class ServiceCache
    {
        public static Resume AddToCache(this Resume resume, string username)
        {
            Common.Service.ServiceCache.AddToCache(UniqueHash(username), resume);
            
            return resume;
        }

        public static bool IsInCache(string firstnameLastname)
        {
            return Common.Service.ServiceCache.IsInCache(UniqueHash(firstnameLastname));
        }

        private static string UniqueHash(string key)
        {
            return string.Format("{0}-{1}", "resume", key).ToLower().Replace(" ", "-");
        }

        public static Resume GetFromCache(string key)
        {
            return Common.Service.ServiceCache.GetFromCache(UniqueHash(key)) as Resume;
        }
    }
}
