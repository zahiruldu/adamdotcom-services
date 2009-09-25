using System;
using AdamDotCom.Whois.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class WhoisServiceTranslatorTests
    {
        [Test]
        public void ShouldVerifyProfileCanBeFoundMultipleResults()
        {
            var whoisServiceTranslator = new WhoisServiceTranslator("68.146.10.100");

            Assert.IsNotNull(whoisServiceTranslator);
            foreach (var item in whoisServiceTranslator.Errors)
            {
                Console.WriteLine(item.Value);
            }
            Assert.IsTrue(whoisServiceTranslator.Errors.Count == 0);

            Response response = (Response) whoisServiceTranslator.GetResponse();

            Assert.AreEqual("CA", response.Country);
            Assert.AreEqual("AB", response.Region);
            Assert.AreEqual("Calgary", response.City);
            Assert.IsTrue(response.Organization.ToLower().Contains("shaw"), response.Organization);
        }
    }
}