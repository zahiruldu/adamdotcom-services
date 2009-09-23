using System;
using NUnit.Framework;

namespace AdamDotCom.Resume.Service.Integration.Tests
{
    [TestFixture]
    public class ResumeServiceTests
    {
        [Test]
        //ToDo: Start WebDev.WebServer.exe before the tests are run in TestSetup then delete this test
        public void SanityTest()
        {
            var resumeService = new ResumeService();

            try
            {
                resumeService.ResumeXml("Adam-Kahtava");
            }
            catch (Exception)
            {
                Assert.Fail("WebDev.WebServer.exe (cassini) probably isn't running, try starting up the ServiceHost project.");
            }
        }

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
