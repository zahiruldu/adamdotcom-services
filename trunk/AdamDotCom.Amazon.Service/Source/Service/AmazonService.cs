using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Service.Extensions;
using AdamDotCom.Common.Service.Infrastructure;

namespace AdamDotCom.Amazon.Service
{
    public class AmazonService : IAmazon
    {
        public Reviews ReviewsByCustomerIdXml(string customerId)
        {
            return Reviews(customerId);
        }

        public Reviews ReviewsByCustomerIdJson(string customerId)
        {
            return Reviews(customerId);
        }

        public Reviews ReviewsByUsernameXml(string username)
        {
            return Reviews(DiscoverUser(username).CustomerId);
        }

        public Reviews ReviewsByUsernameJson(string username)
        {
            return Reviews(DiscoverUser(username).CustomerId);
        }

        public Wishlist WishlistByListIdXml(string listId)
        {
            return Wishlist(listId);
        }

        public Wishlist WishlistByListIdJson(string listId)
        {
            return Wishlist(listId);
        }

        public Wishlist WishlistByUsernameXml(string username)
        {
            return Wishlist(DiscoverUser(username).ListId);
        }

        public Wishlist WishlistByUsernameJson(string username)
        {
            return Wishlist(DiscoverUser(username).ListId);
        }

        public Profile DiscoverUsernameXml(string username)
        {
            return DiscoverUser(username);
        }

        public Profile DiscoverUsernameJson(string username)
        {
            return DiscoverUser(username);
        }

        private static Reviews Reviews(string customerId)
        {
            AssertValidInput(customerId, "customerId");

            if(ServiceCache.IsInCache(customerId))
            {
                var cachedReviews = (Reviews) ServiceCache.GetFromCache(customerId);
                if (cachedReviews != null)
                {
                    return cachedReviews;
                }
            }
            
            var amazonResponse = new AmazonFactory(BuildRequest(customerId, null)).GetResponse();

            HandleErrors(amazonResponse.Errors);

            return new Reviews(amazonResponse.Reviews.OrderByDescending(r => r.Date)).AddToCache(customerId);
        }

        private static Wishlist Wishlist(string listId)
        {
            AssertValidInput(listId, "listId");

            if(ServiceCache.IsInCache(listId))
            {
                var cachedWishlist = (Wishlist) ServiceCache.GetFromCache(listId);
                if (cachedWishlist != null)
                {
                    return cachedWishlist;
                }
            }

            var amazonResponse = new AmazonFactory(BuildRequest(null, listId)).GetResponse();

            HandleErrors(amazonResponse.Errors);

            return new Wishlist(amazonResponse.Products.OrderBy(p => p.AuthorsMLA).ThenBy(p => p.Title)).AddToCache(listId);
        }

        private static Profile DiscoverUser(string username)
        {
            AssertValidInput(username, "username");

            username = Scrub(username);

            if (ServiceCache.IsInCache(MakeUnique(username)))
            {
                var cachedProfile = (Profile) ServiceCache.GetFromCache(MakeUnique(username));
                if (cachedProfile != null)
                {
                    return cachedProfile;
                }
            }

            var sniffer = new ProfileSniffer(username);

            var profile = sniffer.GetProfile();
            
            HandleErrors(sniffer.Errors);

            return profile.AddToCache(MakeUnique(username));
        }

        private static string Scrub(string username)
        {
            return username.Replace(" ", "%20").Replace("-", " ");
        }

        private static AmazonRequest BuildRequest(string customerId, string listId)
        {
            return new AmazonRequest
            {
                AssociateTag = ConfigurationManager.AppSettings["AmazonAssociateTag"],
                AccessKeyId = ConfigurationManager.AppSettings["AmazonAccessKey"],
                CustomerId = customerId,
                ListId = listId,
                SecretAccessKey = ConfigurationManager.AppSettings["AmazonSecretAccessKey"]
            };
        }

        private static void AssertValidInput(string inputValue, string inputName)
        {
            inputName = (string.IsNullOrEmpty(inputName) ? "Unknown" : inputName);

            if (string.IsNullOrEmpty(inputValue) || inputValue.Equals("null", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new RestException(new KeyValuePair<string, string>(inputName, string.Format("{0} is not a valid value.", inputValue)));
            }
        }

        private static void HandleErrors(List<KeyValuePair<string, string>> errors)
        {
            if(errors != null && errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, errors, (int)ErrorCode.InternalError);
            }
        }

        private static string MakeUnique(string key)
        {
            return string.Format("{0}-{1}", "amazon", key).ToLower().Replace(" ", "-");
        }
    }
}