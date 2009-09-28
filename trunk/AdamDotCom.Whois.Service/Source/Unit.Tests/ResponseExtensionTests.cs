using AdamDotCom.Whois.Service;
using AdamDotCom.Whois.Service.Extensions;
using NUnit.Framework;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    [TestFixture]
    public class ResponseExtensionTests
    {
        [Test]
        public void ShouldVerifyFriendlyByOrganization()
        {
            var response = new Response {Organization = "Google Inc."};

            response.ProcessFriendly(string.Empty);
            
            Assert.IsTrue(response.IsFriendly);
            Assert.Contains("google",response.FriendlyMatches);
        }

        [Test]
        public void ShouldVerifyFriendlyByReferrer()
        {
            var response = new Response();

            response.ProcessFriendly("Twitter");

            Assert.IsTrue(response.IsFriendly);
            Assert.Contains("twitter", response.FriendlyMatches);
        }

        [Test]
        public void ShouldVerifyFiltersByCountryAndReferrer()
        {
            var response = new Response {Country = "CANADA"};

            response.ProcessFilters("Canada,github", "github");

            Assert.IsTrue(response.IsFilterMatch);
            Assert.Contains("Country", response.FilterMatches);
            Assert.Contains("Referrer", response.FilterMatches);
        }
    }
}