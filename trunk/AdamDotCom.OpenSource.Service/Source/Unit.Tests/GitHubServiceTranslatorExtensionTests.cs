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
        public void VerifyGetProjects()
        {
            var result = new Projects().GetProjects(TestHelper.PageSourceGitHubAdamDotCom_Xml);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count != 0);
            
            Console.WriteLine(result[0].Description);

            Assert.IsTrue(result[0].Name == "scripts");
        }

        [Test]
        public void VerifyGetProjectDetails()
        {
            var project = new Project { Name = "project-badge" };
            var result = project.GetDetails(TestHelper.PageSourceGitHubAdamDotComProjectBadge_Xml);

            Assert.IsNotNull(result);

            Console.WriteLine(result.LastModified);
            Console.WriteLine(result.LastMessage);

            Assert.IsFalse(string.IsNullOrEmpty(result.LastModified));
            Assert.IsFalse(string.IsNullOrEmpty(result.LastMessage));
        }
    }
}