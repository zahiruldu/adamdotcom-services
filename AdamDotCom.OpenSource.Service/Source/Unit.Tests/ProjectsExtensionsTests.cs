using System;
using AdamDotCom.OpenSource.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    [TestFixture]
    public class ProjectsExtensionsTests
    {
        [Test]
        public void ShouldVerifyFilters()
        {
            var projects = new Projects
                               {
                                   new Project { Name = "adamdotcom-services", LastModified = DateTime.Now.AddDays(-1).ToString(), Url = "project1" },
                                   new Project { Name = "services", LastModified = DateTime.Now.ToString(), Url = "project2" },
                                   new Project { Name = "-services-", LastModified = DateTime.Now.AddDays(-2).ToString(), Url = "project3" }
                               };

            foreach (var project in projects.Filter("remove:adamdotcom"))
            {
                Assert.IsTrue(project.Name.IndexOf("adamdotcom") == -1);
            }
            foreach (var project in projects.Filter("remove:-"))
            {
                Assert.IsTrue(project.Name.IndexOf("-") == -1);
                Assert.IsFalse(project.Name == "adamdotcomservices");
                Assert.IsFalse(project.Name == " services ");
            }

            var result = projects.Filter("remove:duplicate-items");
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].LastModified == projects[1].LastModified);
            Assert.IsTrue(result[0].Url == projects[1].Url);
        }
    }
}