using AdamDotCom.Resume.Service.Proxy;
using NUnit.Framework;

namespace AdamDotCom.Resume.Service.Integration.Tests
{
    [TestFixture]
    public class ResumeServiceTests
    {
        [Test]
        public void ShouldVerifyProxyAndReturnResume()
        {
            var amazonService = new ResumeService();

            var resume = amazonService.ResumeXml("Adam Kahtava");

            Assert.IsNotNull(resume);
            Assert.IsNotNull(resume.Positions);
            Assert.Greater(resume.Positions.Count, 1);

            resume = amazonService.ResumeJson("Adam Kahtava");

            Assert.IsNotNull(resume);
            Assert.IsNotNull(resume.Positions);
            Assert.Greater(resume.Positions.Count, 1);
        }
    }
}
