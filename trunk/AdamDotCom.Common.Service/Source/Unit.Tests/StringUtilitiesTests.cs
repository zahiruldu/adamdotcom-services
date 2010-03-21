using AdamDotCom.Common.Service.Utilities;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class StringUtilitiesTests
    {
        [Test]
        public void Test()
        {
            Assert.IsTrue("twist-twist-flip-twist-kahtava".Has("KAHTAVA"));
            Assert.IsFalse("twist-twist-flip-twist-kahtava".Has("umpa-lumpa"));
        }
    }
}
