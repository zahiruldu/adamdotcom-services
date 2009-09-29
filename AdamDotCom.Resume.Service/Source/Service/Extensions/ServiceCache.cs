namespace AdamDotCom.Resume.Service.Extensions
{
    public static class ServiceCache
    {
        public static Resume AddToCache(this Resume resume, string username)
        {
            Common.Service.ServiceCache.AddToCache(username, resume);
            
            return resume;
        }
    }
}
