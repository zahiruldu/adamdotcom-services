using System.Web;
using AdamDotCom.OpenSource.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class OpenSourceServiceTests
    {
        [Test]
        public void ShouldVerify_ParseProjectHostAndUsername()
        {
            var result = ProjectHostExtensions.ParseProjectHostAndUsername("github:adamdotcom,googlecode:adam.kahtava.com");

            Assert.IsNotNull(result);
            Assert.IsTrue(result[0].Key == "github");
            Assert.IsTrue(result[0].Value == "adamdotcom");
            Assert.IsTrue(result[1].Key == "googlecode");
            Assert.IsTrue(result[1].Value == "adam.kahtava.com");
        }

        [Test]
        public void ShouldVerify_ParseProjectHostAndUsernameWithTrailingComma()
        {
            var result = ProjectHostExtensions.ParseProjectHostAndUsername("github:adamdotcom,googlecode:adam.kahtava.com,");

            Assert.IsNotNull(result);
            Assert.IsTrue(result[0].Key == "github");
            Assert.IsTrue(result[0].Value == "adamdotcom");
            Assert.IsTrue(result[1].Key == "googlecode");
            Assert.IsTrue(result[1].Value == "adam.kahtava.com");
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldVerify_ThrowException1()
        {
            ProjectHostExtensions.ParseProjectHostAndUsername("github,adam.kahtava.com");
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldVerify_ThrowException2()
        {
            new OpenSourceService().GetProjectsByProjectHostAndUsernameXml("github:adamdotcom,googlecode:");
        }
    }
}