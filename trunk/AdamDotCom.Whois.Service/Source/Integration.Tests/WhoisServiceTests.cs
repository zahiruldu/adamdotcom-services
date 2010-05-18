using System;
using NUnit.Framework;
using AdamDotCom.Whois.Service.Proxy;

namespace AdamDotCom.Whois.Service.Integration.Tests
{
    [TestFixture]
    public class WhoisServiceTests
    {
        [Test]
        //ToDo: Start WebDev.WebServer.exe before the tests are run in TestSetup then delete this test
        public void SanityTest()
        {
            var WhoisService = new WhoisService();

            try
            {
                WhoisService.WhoisXml(null);
            }
            catch(Exception)
            {
                Assert.Fail("WebDev.WebServer.exe (cassini) probably isn't running, try starting up the ServiceHost project.");
            }
        }

        [Test]
        public void ShouldVerifyProxyAndExerciseWhois()
        {
            var WhoisService = new WhoisService();

            var responseXml = WhoisService.WhoisXml("68.146.10.100");

            Assert.IsNotNull(responseXml);
            Assert.AreEqual("68.146.10.100", responseXml.DomainName);

            var responseJson = WhoisService.WhoisJson("68.146.10.100");

            Assert.IsNotNull(responseJson);
            Assert.AreEqual("68.146.10.100", responseJson.DomainName);
        }

        [Test]
        public void ShouldVerifyProxyAndExerciseWhoisEnhanced()
        {
            var WhoisService = new WhoisService();

            var responseXml = WhoisService.WhoisEnhancedXml("68.146.10.100", "alberta", "google");

            Assert.IsNotNull(responseXml);
            Assert.AreEqual("Canada", responseXml.Country);

            var responseJson = WhoisService.WhoisEnhancedJson("68.146.10.100", "CA", "twitter");

            Assert.IsNotNull(responseJson);
            Assert.AreEqual("Canada", responseJson.Country);
        }
    }
}
