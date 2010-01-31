using System;
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
   }
}