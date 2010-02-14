using System;
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
            var service = new OpenSourceService();
            var result = service.ParseProjectHostAndUsername("github:adamdotcom,googlecode:adam.kahtava.com");

            Assert.IsNotNull(result);
            Assert.IsTrue(result[0].Key == "github");
            Assert.IsTrue(result[0].Value == "adamdotcom");
            Assert.IsTrue(result[1].Key == "googlecode");
            Assert.IsTrue(result[1].Value == "adam.kahtava.com");
        }

        [Test]
        public void ShouldVerify_ParseProjectHostAndUsernameWithTrailingComma()
        {
            var service = new OpenSourceService();
            var result = service.ParseProjectHostAndUsername("github:adamdotcom,googlecode:adam.kahtava.com,");

            Assert.IsNotNull(result);
            Assert.IsTrue(result[0].Key == "github");
            Assert.IsTrue(result[0].Value == "adamdotcom");
            Assert.IsTrue(result[1].Key == "googlecode");
            Assert.IsTrue(result[1].Value == "adam.kahtava.com");
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldVerify_ThrowException1()
        {
            new OpenSourceService().ParseProjectHostAndUsername("github,adam.kahtava.com");
        }

        [Test, ExpectedException(typeof(HttpException))]
        public void ShouldVerify_ThrowException2()
        {
            new OpenSourceService().GetProjectsByProjectHostAndUsernameXml("github:adamdotcom,googlecode:");
        }

        [Test]
        public void ShouldVerify_BuildHtml()
        {
            var projects = new Projects
                               {
                                   new Project
                                       {
                                           Description = "my desc1",
                                           LastMessage = "commit1",
                                           LastModified = "2010-02-14",
                                           Name = "my project name1",
                                           Url = "http://code.google.com"
                                       },
                                   new Project
                                       {
                                           Description = "my des2",
                                           LastMessage = "commit2",
                                           LastModified = "2010-02-13",
                                           Name = "my project name2",
                                           Url = "http://github.com"
                                       }
                               };
            var resultsHtml = new OpenSourceService().BuildHtml(projects);

            Assert.IsNotNull(resultsHtml);
            Assert.IsTrue(resultsHtml.Contains("google"));
            Assert.IsTrue(resultsHtml.Contains("github"));
            Console.WriteLine(resultsHtml);
        }
    }
}