using System;
using System.Linq;
using System.Collections.Generic;
using AdamDotCom.OpenSource.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class GoogleCodeWebSnifferTests
    {
        [Test]
        public void VerifyParseProjects()
        {
            List<Project> projects = new List<Project>();
            
            projects = projects.ParseProjects(TestHelper.PageSourceGoogleCodeAdamKahtavaCom_ProfileWebsite);

            Assert.IsNotNull(projects);
            
            Console.WriteLine(projects[0].Name + " " + projects[0].Url);
            Assert.IsTrue(projects.Where(p => p.Name == "/p/adamdotcom-website/").FirstOrDefault() != null);
        }

        [Test]
        public void VerifyClean()
        {
            var projects = new List<Project> { new Project { Name = "/p/adamdotcom-website/", Url = "http://code.google.com/feeds/p/adamdotcom-website/" } };
            projects = projects.Clean();

            Console.WriteLine(projects[0].Name);
            Console.WriteLine(projects[0].Url);

            Assert.IsTrue(projects[0].Name == "adamdotcom-website");
            Assert.IsTrue(projects[0].Url == "http://code.google.com/feeds/p/adamdotcom-website");
        }

        [Test]
        public void VerifyParseDetails()
        {
            var project = new Project();
            project = project.ParseDetails(TestHelper.PageSourceGoogleCodeAdamKahtavaCom_ProjectWebsite);

            Console.WriteLine(project.Description);
            Assert.AreEqual("The site source in use on Adam.Kahtava.com / AdamDotCom.com (http://adam.kahtava.com/)", project.Description);
        }

        [Test]
        public void VerifyCleanCommitMessage()
        {
            var result = @"<span class=""ot-logmessage"">Deleted and moved Amazon.Service to http://code.google.com/p/adamdotcom-services/<br></span>".CleanCommitMessage();

            Console.WriteLine(result); 
            
            Assert.IsFalse(result.Contains(">"));
        }

        [Test]
        public void VerifyCleanCommitMessage2()
        {
            var result = @"<div class=""ot-issue-fields""><div class=""ot-issue-field-wrapper""><span class=""ot-issue-field-name"">Status: </span><span class=""ot-issue-field-value"">Fixed</span></div></div>".CleanCommitMessage();

            Console.WriteLine(result); 
            
            Assert.IsFalse(result.Contains(">"));
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }       

        [Test]
        public void VerifyParseLastModifedDateAndLastMessage()
        {
            var project = new Project();
            var result = project.ParseLastModifiedDate(TestHelper.PageSourceGoogleCodeAdamKahtavaCom_ServicesXML);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(null, result.LastMessage);
            Console.WriteLine(result.LastMessage);
            Assert.AreNotEqual(null, result.LastModified);
            Console.WriteLine(result.LastModified);
        }
    }
}