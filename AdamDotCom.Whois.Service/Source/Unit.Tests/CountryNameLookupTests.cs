using NUnit.Framework;

namespace AdamDotCom.Whois.Service.Unit.Tests
{
    [TestFixture]
    public class CountryNameLookupTests
    {
        [Test]
        public void ShouldVerifyTranslatorWorks()
        {
            var countryTranslator = new CountryNameLookup.CountryNameLookup();

            Assert.IsNotNull(countryTranslator);
            Assert.AreEqual("Canada", countryTranslator.GetCountryName("CA"));
        }
    }
}