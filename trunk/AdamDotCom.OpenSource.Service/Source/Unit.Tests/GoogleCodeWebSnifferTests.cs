using System;
using System.Collections.Generic;
using AdamDotCom.OpenSource.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class GoogleCodeWebSnifferTests
    {
        [Test]
        public void ShouldVerify_GetProjects()
        {
            var projectSniffer = new GoogleCodeWebSniffer();
            
            List<Project> projects = projectSniffer.GetProjects(TestHelper.PageSourceGoogleCodeAdamKahtavaCom_ProfileWebsite);

            Assert.IsNotNull(projects);
            
            Console.WriteLine(projects[0].Name + " " + projects[0].Url);
            Assert.AreEqual("/p/adamdotcom-website/", projects[0].Name);
        }

        [Test]
        public void ShouldVerify_FixNames()
        {
            var projectSniffer = new GoogleCodeWebSniffer();

            var projects = new List<Project> {new Project {Name = "/p/adamdotcom-website/"}};
            projectSniffer.Clean(projects);

            Console.WriteLine(projects[0].Name);
            Assert.IsTrue(projects[0].Name == "adamdotcom-website");
        }

        [Test]
        public void ShouldVerify_GetProjectDetail()
        {
            var profileSniffer = new GoogleCodeWebSniffer();

            var result = profileSniffer.GetProjectDetail(new Project(), TestHelper.PageSourceGoogleCodeAdamKahtavaCom_ProjectWebsite);

            Console.WriteLine(result.Description);
            Assert.AreEqual("The site source in use on Adam.Kahtava.com / AdamDotCom.com (http://adam.kahtava.com/)", result.Description);
        }

        [Test]
        public void ShouldVerify_GetProjectDetails()
        {
            var projects = new List<Project>
                               {
                                   new Project
                                       {
                                           Name = "adamdotcom-website",
                                           Url = "http://code.google.com/p/adamdotcom-website/"
                                       },
                                   new Project
                                       {
                                           Name = "adamdotcom-services",
                                           Url = "http://code.google.com/p/adamdotcom-services/"
                                       }
                               };
            
            var profileSniffer = new GoogleCodeWebSniffer();
            var results = profileSniffer.GetProjectDetails(projects);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count != 0);
            Assert.AreEqual("The site source in use on Adam.Kahtava.com / AdamDotCom.com (http://adam.kahtava.com/)", results[0].Description);
            Assert.AreEqual("The services in use on Adam.Kahtava.com / AdamDotCom.com (http://adam.kahtava.com/services/)", results[1].Description);
        }
    }
}