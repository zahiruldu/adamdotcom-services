using System;
using System.ServiceModel;
using AdamDotCom.OpenSource.Service.Proxy;
using NUnit.Framework;

namespace Integration.Tests
{
    [TestFixture]
    public class OpenSourceServiceTests
    {
        [Test]
        //ToDo: Start WebDev.WebServer.exe before the tests are run in TestSetup then delete this test
        public void SanityTest()
        {
            var service = new OpenSourceService();

            try
            {
                service.GetProjectsByUsernameXml(ProjectHost.GitHub.ToString(), "AdamDotCom");
            }
            catch(Exception)
            {
                Assert.Fail("WebDev.WebServer.exe (cassini) probably isn't running, try starting up the ServiceHost project.");
            }
        }

        [Test]
        public void ShouldVerifyProxyAndGetProjectsByUsername()
        {
            var service = new OpenSourceService();

            var resultsXml = service.GetProjectsByUsernameXml(ProjectHost.GitHub.ToString(), "AdamDotCom");

            Assert.IsNotNull(resultsXml);
            Assert.Greater(resultsXml.Count, 1);
            
            var resultsJson = service.GetProjectsByUsernameJson(ProjectHost.GoogleCode.ToString(), "adam.kahtava.com");

            Assert.IsNotNull(resultsJson);
            Assert.Greater(resultsXml.Count, 1);
        }

        [Test, ExpectedException(typeof(ProtocolException))]
        public void ShouldVerifyProxyAndThrowException()
        {
            var service = new OpenSourceService();

            service.GetProjectsByUsernameXml(ProjectHost.Unknown.ToString(), "AdamDotCom");
        }

        public void ShouldVerifyProxyAndGetGetProjectsByProjectHostAndUsernameXml()
        {
            var service = new OpenSourceService();

            var resultsXml = service.GetProjectsByProjectHostAndUsernameXml("github:adamdotcom,googlecode:adam.kahtava.com");

            Assert.IsNotNull(resultsXml);
            Assert.Greater(resultsXml.Count, 1);

            var resultsJson = service.GetProjectsByProjectHostAndUsernameJson("github:adamdotcom,googlecode:adam.kahtava.com");

            Assert.IsNotNull(resultsJson);
            Assert.Greater(resultsJson.Count, 1);
        }
    }
}