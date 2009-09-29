namespace AdamDotCom.Amazon.Service.Extensions
{
    public static class ServiceCache
    {

        public static Wishlist AddToCache(this Wishlist wishlist, string listId)
        {
            Common.Service.ServiceCache.AddToCache(listId, wishlist);
            
            return wishlist;
        }

        public static Reviews AddToCache(this Reviews reviews, string customerId)
        {
            Common.Service.ServiceCache.AddToCache(customerId, reviews);

            return reviews;
        }

        public static Profile AddToCache(this Profile profile, string username)
        {
            Common.Service.ServiceCache.AddToCache(username, profile);

            return profile;
        }
    }
}
