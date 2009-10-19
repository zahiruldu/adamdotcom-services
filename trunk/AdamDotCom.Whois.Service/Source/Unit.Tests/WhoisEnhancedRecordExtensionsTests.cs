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
            var response = new WhoisEnhancedRecord {Organization = "Google Inc."};

            response.ProcessFriendly();
            
            Assert.IsTrue(response.IsFriendly);
            Assert.Contains("google",response.FriendlyMatches);
        }

        [Test]
        public void ShouldVerifyReferrerByReferrer()
        {
            var response = new WhoisEnhancedRecord();

            response.ProcessReferrer("Twitter");

            Assert.IsTrue(response.IsReferrerMatch);
            Assert.Contains("twitter", response.ReferrerMatches);
        }

        [Test]
        public void ShouldVerifyFiltersByCountryAndReferrer()
        {
            var response = new WhoisEnhancedRecord {Country = "CANADA"};

            response.ProcessFilters("Canada,github", "github");

            Assert.IsTrue(response.IsFilterMatch);
            Assert.Contains("Country", response.FilterMatches);
            Assert.Contains("Referrer", response.FilterMatches);
        }
    }
}