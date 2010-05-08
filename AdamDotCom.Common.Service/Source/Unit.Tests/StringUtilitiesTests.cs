using System;
using AdamDotCom.Common.Service.Utilities;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class StringUtilitiesTests
    {
        [Test]
        public void ShouldTestHas()
        {
            Assert.IsTrue("twist-twist-flip-twist-kahtava".Has("KAHTAVA"));
            Assert.IsFalse("twist-twist-flip-twist-kahtava".Has("umpa-lumpa"));
        }

        [Test]
        public void ShouldTestScrub()
        {
            var dirtyStrings = new[] {"%20", "-", "\r\n", "\n", "\r", "\t", ",,"};
            
            foreach (var dirtyString in dirtyStrings)
            {
                Console.WriteLine(dirtyString);
                Assert.IsFalse(dirtyString.Scrub().Has(dirtyString));
            }
        }
    }
}
