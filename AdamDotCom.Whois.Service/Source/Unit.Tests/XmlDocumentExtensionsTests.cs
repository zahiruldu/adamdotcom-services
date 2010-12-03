using System.Xml;
using AdamDotCom.Whois.Service.Extensions;
using NUnit.Framework;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    [TestFixture]
    public class XmlDocumentExtensionsTests
    {
        [Test]
        public void VerifyWasLoaded()
        {
            Assert.IsFalse(new XmlDocument().WasLoaded());
            
            XmlDocument xmlDocument = null;
            Assert.IsFalse(xmlDocument.WasLoaded());
        }
    }
}
