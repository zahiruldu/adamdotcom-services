using System;
using System.Configuration;
using NUnit.Framework;

namespace AdamDotCom.Resume.Service.Integration.Tests
{
    [TestFixture]
    public class LinkedInResumeSnifferTests
    {
        private LinkedInResumeSniffer resumeSnifferWithStaticPageSource;
        private readonly string linkedInEmailAddress = ConfigurationManager.AppSettings["LinkedInEmailAddress"];
        private readonly string linkedInPassword = ConfigurationManager.AppSettings["LinkedInPassword"];

        [Test]
        public void ShouldVerifyCustomerIdCanBeFoundMultipleResults()
        {
            resumeSnifferWithStaticPageSource = new LinkedInResumeSniffer(linkedInEmailAddress, linkedInPassword, "Adam Kahtava");

            foreach (var error in resumeSnifferWithStaticPageSource.Errors)
            {
                Console.WriteLine(error);
            }

            Assert.AreEqual(0, resumeSnifferWithStaticPageSource.Errors.Count);

            var resume = resumeSnifferWithStaticPageSource.GetResume();

            foreach (var error in resumeSnifferWithStaticPageSource.Errors)
            {
                Console.WriteLine(error);
            }

            Assert.AreEqual(0, resumeSnifferWithStaticPageSource.Errors.Count);
        }
    }
}