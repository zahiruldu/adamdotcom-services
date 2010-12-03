using AdamDotCom.Whois.Service.Extensions;
using NUnit.Framework;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    [TestFixture]
    public class WhoisRecordExtensionsTests
    {
        [Test]
        public void VerifyWhoisResultIsTranslated()
        {
            var response = new WhoisRecord().Translate("68.146.10.100", TestHelper.SearchResult, TestHelper.ShawcResult, TestHelper.PocsResult, TestHelper.Pocs);

            Assert.IsNotNull(response);

            Assert.IsNotNull(response.DomainName);
            Assert.IsNotNull(response.RegistryData);
            Assert.AreEqual("CA", response.RegistryData.Registrant.Country);
            Assert.AreEqual("AB", response.RegistryData.Registrant.StateProv);
            Assert.AreEqual("Calgary", response.RegistryData.Registrant.City);
            Assert.AreEqual("Suite 800, 630 - 3rd Ave. SW", response.RegistryData.Registrant.Address);
            Assert.IsNotNull(response.RegistryData.AbuseContact);
            Assert.AreEqual("+1-403-750-7420", response.RegistryData.AbuseContact.Phone);
            Assert.AreEqual("internet.abuse@sjrb.ca", response.RegistryData.AbuseContact.Email);
            Assert.AreEqual("SHAW ABUSE", response.RegistryData.AbuseContact.Name);
            Assert.IsNotNull(response.RegistryData.AdministrativeContact);
            Assert.IsNotNull(response.RegistryData.TechnicalContact);
        }
    }
}
