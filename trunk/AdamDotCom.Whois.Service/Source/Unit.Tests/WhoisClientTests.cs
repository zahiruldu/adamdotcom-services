using System;
using AdamDotCom.Whois.Service.WhoisClient;
using NUnit.Framework;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    [TestFixture]
    public class WhoisClientTests
    {
        [Test]
        public void ShouldVerifyRawWhoisResultIsParsed()
        {
            var response = WhoisRecordExtensions.Translate("68.146.10.100", TestHelper.WhoisRawResult1);
            Assert.IsNotNull(response);

            Assert.IsNotNull(response.DomainName);
            Assert.IsNotNull(response.RegistryData);
            Assert.IsTrue(response.RegistryData.Registrant.Country == "CA");
            Assert.IsTrue(response.RegistryData.Registrant.StateProv == "AB");
            Assert.IsTrue(response.RegistryData.Registrant.City == "Calgary");
            Assert.IsNotNull(response.RegistryData.AbuseContact);
            Assert.IsNotNull(response.RegistryData.TechnicalContact);
            Assert.IsNotNull(response.RegistryData.RawText);
        }

        [Test, Ignore("Removed DNS resolution capabilities")]
        public void ShouldVerifyRawWhoisResultDomainNameRecordIsParsed()
        {
            var response = WhoisRecordExtensions.Translate("adamdotcom.com", TestHelper.WhoisRawResult2);
            Assert.IsNotNull(response);

            Assert.IsNotNull(response.DomainName);
            Assert.IsNotNull(response.RegistryData);
            Assert.IsTrue(response.RegistryData.Registrant.Country == "CA");
            Assert.IsTrue(response.RegistryData.Registrant.StateProv == "ON");
            Assert.IsTrue(response.RegistryData.Registrant.City == "Ottawa");
            Assert.IsNotNull(response.RegistryData.AdministrativeContact);
            Assert.IsNotNull(response.RegistryData.TechnicalContact);
            Assert.IsNotNull(response.RegistryData.RawText);
        }

        [Test]
        public void ShouldVerifyWhoisClientWorksOnShaw()
        {
            var whoisClient = new WhoisClient.WhoisClient("68.146.10.100");

            Assert.IsNotNull(whoisClient);
            foreach (var item in whoisClient.Errors)
            {
                Console.WriteLine(item.Value);
            }
            Assert.IsTrue(whoisClient.Errors.Count == 0);

            var response = whoisClient.GetWhoisRecord();

            Assert.AreEqual("CA", response.RegistryData.Registrant.Country);
            Assert.AreEqual("AB", response.RegistryData.Registrant.StateProv);
            Assert.AreEqual("Calgary", response.RegistryData.Registrant.City);
            Assert.IsTrue(response.RegistryData.Registrant.Name.ToLower().Contains("shaw"));
        }

        [Test]
        public void ShouldVerifyWhoisClientWorksOnGoogle()
        {
            WhoisRecord response;
            var whoisClient = new WhoisClient.WhoisClient("74.125.127.99");

            Assert.IsNotNull(whoisClient);
            foreach (var item in whoisClient.Errors)
            {
                Console.WriteLine(item.Value);
            }
            Assert.IsTrue(whoisClient.Errors.Count == 0);

            response = whoisClient.GetWhoisRecord();
            
            Assert.AreEqual("US", response.RegistryData.Registrant.Country);
            Assert.AreEqual("CA", response.RegistryData.Registrant.StateProv);
            Assert.AreEqual("Mountain View", response.RegistryData.Registrant.City);
            Assert.IsTrue(response.RegistryData.Registrant.Name.ToLower().Contains("google"));
        }

        [Test, Ignore("Removed DNS resolution capabilities")]
        public void ShouldVerifyWhoisClientWorksOnAdamDotCom()
        {
            var whoisClient = new WhoisClient.WhoisClient("kahtava.com");

            Assert.IsNotNull(whoisClient);
            foreach (var item in whoisClient.Errors)
            {
                Console.WriteLine(item.Value);
            }
            Assert.IsTrue(whoisClient.Errors.Count == 0);

            var response = whoisClient.GetWhoisRecord();

            Assert.IsNotNull(response);
            Assert.AreEqual("CA", response.RegistryData.Registrant.Country);
            Assert.AreEqual("ON", response.RegistryData.Registrant.StateProv);
            Assert.AreEqual("Ottawa", response.RegistryData.Registrant.City);
            Assert.IsTrue(response.DomainName.ToLower().Contains("kahtava"));
        }
    }
}   