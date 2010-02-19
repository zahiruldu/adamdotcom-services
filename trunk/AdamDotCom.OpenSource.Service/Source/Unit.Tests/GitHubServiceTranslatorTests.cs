using System;
using System.Collections.Generic;
using AdamDotCom.OpenSource.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class GitHubServiceTranslatorTests
    {
        [Test]
        public void ShouldVerify_GetProjects()
        {
            var gitHubServiceTranslator = new GitHubServiceTranslator();

            var result = gitHubServiceTranslator.GetProjects(TestHelper.PageSourceGitHubAdamDotCom_Xml);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count != 0);
            
            Console.WriteLine(result[0].Description);

            Assert.IsTrue(result[0].Name == "scripts");
        }

        //Note: This is really an integration test
        [Test]
        public void ShouldVerify_GetProjectDetails()
        {
            var gitHubServiceTranslator = new GitHubServiceTranslator();

            var projects = new List<Project> { new Project { Name = "project-badge" } };
            var result = gitHubServiceTranslator.GetProjectDetails(projects, "adamdotcom");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count != 0);

            Console.WriteLine(result[0].LastModified);
            Console.WriteLine(result[0].LastMessage);

            Assert.IsFalse(string.IsNullOrEmpty(result[0].LastModified));
            Assert.IsFalse(string.IsNullOrEmpty(result[0].LastMessage));
        }
    }
}