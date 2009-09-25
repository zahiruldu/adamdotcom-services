using System;
using NUnit.Framework;
using AdamDotCom.Whois.Service.Proxy;

namespace AdamDotCom.Whois.Service.Integration.Tests
{
    [TestFixture]
    public class WhoisServiceTests
    {
//        [Test]
//        //ToDo: Start WebDev.WebServer.exe before the tests are run in TestSetup then delete this test
//        public void SanityTest()
//        {
//            var WhoisService = new WhoisService();
//
//            try
//            {
//                WhoisService.DiscoverUsernameXml("Adam-Kahtava");
//            }
//            catch(Exception)
//            {
//                Assert.Fail("WebDev.WebServer.exe (cassini) probably isn't running, try starting up the ServiceHost project.");
//            }
//        }
//
//        [Test]
//        public void ShouldVerifyProxyAndReturnReviews()
//        {
//            var WhoisService = new WhoisService();
//
//            var reviewsFromXmlRequest = WhoisService.ReviewsByCustomerIdXml("A2JM0EQJELFL69");
//
//            Assert.IsNotNull(reviewsFromXmlRequest);
//            Assert.Greater(reviewsFromXmlRequest.Count, 1);
//            
//            var reviewsFromJsonRequest = WhoisService.ReviewsByCustomerIdJson("A2JM0EQJELFL69");
//
//            Assert.IsNotNull(reviewsFromJsonRequest);
//            Assert.AreEqual(reviewsFromXmlRequest.Count, reviewsFromJsonRequest.Count);
//        }
//
//        [Test]
//        public void ShouldVerifyProxyAndReturnWishlist()
//        {
//            var WhoisService = new WhoisService();
//
//            var wishlistFromXmlRequest = WhoisService.WishlistByListIdXml("3JU6ASKNUS7B8");
//
//            Assert.IsNotNull(wishlistFromXmlRequest);
//            Assert.Greater(wishlistFromXmlRequest.Count, 1);
//
//            var wishlistFromJsonRequest = WhoisService.WishlistByListIdXml("3JU6ASKNUS7B8");
//
//            Assert.IsNotNull(wishlistFromJsonRequest);
//            Assert.AreEqual(wishlistFromXmlRequest.Count, wishlistFromJsonRequest.Count);
//        }
//
//        [Test]
//        public void ShouldVerifyProxyAndReturnProfile()
//        {
//            var WhoisService = new WhoisService();
//
//            var profileFromXmlRequest = WhoisService.DiscoverUsernameXml("Adam-Kahtava");
//
//            Assert.IsNotNull(profileFromXmlRequest);
//            Assert.IsFalse(string.IsNullOrEmpty(profileFromXmlRequest.CustomerId));
//            Assert.IsFalse(string.IsNullOrEmpty(profileFromXmlRequest.ListId));
//
//            var profileFromJsonRequest = WhoisService.DiscoverUsernameJson("Adam-Kahtava");
//
//            Assert.AreEqual(profileFromXmlRequest.CustomerId, profileFromJsonRequest.CustomerId);
//            Assert.AreEqual(profileFromXmlRequest.ListId, profileFromJsonRequest.ListId);
//        }
    }
}
