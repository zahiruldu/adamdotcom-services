using System;
using NUnit.Framework;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    [TestFixture]
    public class WhoisClientTests
    {
        [Test]
        public void ShouldVerifyWhoisClientWorksOnGoogle()
        {
            WhoisRecord response;
            var whoisClient = new WhoisClient("74.125.127.99");

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

        [Test]
        public void ShouldVerifyWhoisClientWorksOnShaw()
        {
            var whoisClient = new WhoisClient("68.146.10.100");

            Assert.IsNotNull(whoisClient);
            foreach (var item in whoisClient.Errors)
            {
                Console.WriteLine(item.Value);
            }
            Assert.IsTrue(whoisClient.Errors.Count == 0);

            WhoisRecord response = whoisClient.GetWhoisRecord();

            Assert.AreEqual("CA", response.RegistryData.Registrant.Country);
            Assert.AreEqual("AB", response.RegistryData.Registrant.StateProv);
            Assert.AreEqual("Calgary", response.RegistryData.Registrant.City);
            Assert.IsTrue(response.RegistryData.Registrant.Name.ToLower().Contains("shaw"));
        }

        [Test]
        public void VerifyFatalQueryThrowsNoErros()
        {
            try
            {
                new WhoisClient("silly-query-param");
            }
            catch
            {
                Assert.Fail("Eception was not expected");
            }
        }
    }
}