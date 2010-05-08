using System.Web;
using NUnit.Framework;
using Assert = AdamDotCom.Common.Service.Infrastructure.Assert;
namespace Unit.Tests
{
    [TestFixture]
    public class AssertTests
    {
        public enum TestEnum
        {
            Unknown = 0,
            None = 1,
            Something = 2
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_Null()
        {
            Assert.ValidInput("Null", string.Empty);
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_CaseInsensitive()
        {
            Assert.ValidInput("NuLl", string.Empty);
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_Unknown()
        {
            Assert.ValidInput("Unknown", string.Empty);
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_StringEmpty()
        {
            Assert.ValidInput("", string.Empty);
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_None()
        {
            Assert.ValidInput("None", string.Empty);
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_NaN()
        {
            Assert.ValidInput("NaN", string.Empty);
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_Undefined()
        {
            Assert.ValidInput("Undefined", string.Empty);
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldTestAssertion_LiteralStringEmpty()
        {
            Assert.ValidInput("string.Empty", string.Empty);
        }

        [Test]
        public void ShouldTestAssertion_SomethingGood()
        {
            try
            {
                Assert.ValidInput("Something-GOOD!!", string.Empty);
            }
            catch
            {
                NUnit.Framework.Assert.Fail("Unexpected exception");
            }
        }
    }
}
