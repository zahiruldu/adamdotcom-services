using System;
using AdamDotCom.Resume.Service;
using AdamDotCom.Resume.Service.Unit.Tests;
using NUnit.Framework;

namespace AdamDotCom.Resume.Service.Unit.Tests
{
    [TestFixture]
    public class LinkedInResumeSnifferTests
    {
        private LinkedInResumeSniffer resumeSnifferWithStaticPageSource;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            resumeSnifferWithStaticPageSource = new LinkedInResumeSniffer("Adam Kahtava", TestHelper.PageSource1);
        }

        [Test]
        public void ShouldGetSummary()
        {
            var summaries = resumeSnifferWithStaticPageSource.GetSummary();
            Console.WriteLine(summaries);
            Assert.IsFalse(string.IsNullOrEmpty(summaries));
        }

        [Test]
        public void ShouldGetSpecialties()
        {
            var specialties = resumeSnifferWithStaticPageSource.GetSpecialties();
            Console.WriteLine(specialties);
            Assert.IsFalse(string.IsNullOrEmpty(specialties));
        }

        [Test]
        public void ShouldGetPositionTitles()
        {
            var titles = resumeSnifferWithStaticPageSource.GetPositionTitles();
            Assert.IsFalse(titles == null);
            Assert.IsFalse(titles.Count < 0);
            foreach (var item in titles)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ShouldGetPositionCompanies()
        {
            var companies = resumeSnifferWithStaticPageSource.GetPositionCompanies();
            Assert.IsFalse(companies == null);
            Assert.IsFalse(companies.Count < 0);
            foreach (var item in companies)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ShouldGetPositionPeriods()
        {
            var periods = resumeSnifferWithStaticPageSource.GetPositionPeriods();
            Assert.IsFalse(periods == null);
            Assert.IsFalse(periods.Count < 0);
            foreach (var item in periods)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ShouldGetPositionDescriptions()
        {
            var descriptions = resumeSnifferWithStaticPageSource.GetPositionDescriptions();
            foreach (var item in descriptions)
            {
                Console.WriteLine(item);
            }
            Assert.IsFalse(descriptions == null);
            Assert.IsFalse(descriptions.Count < 0);
        }

        [Test]
        public void ShouldGetEducationInstitutes()
        {
            var institutes = resumeSnifferWithStaticPageSource.GetEducationInstitutes();
            Assert.IsFalse(institutes == null);
            Assert.IsFalse(institutes.Count < 0);
            foreach (var item in institutes)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ShouldGetEducationCertificates()
        {
            var certificates = resumeSnifferWithStaticPageSource.GetEducationCertificates();
            Assert.IsFalse(certificates == null);
            Assert.IsFalse(certificates.Count < 0);
            foreach (var item in certificates)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ShouldGetResumeFromPageSource()
        {
            var resumeSniffer = new LinkedInResumeSniffer("Adam Kahtava", TestHelper.PageSource1);

            var resume = resumeSniffer.GetResume();

            Assert.IsNotNull(resume);

            Assert.IsTrue(resume.Positions.Count == 10, resume.Positions.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Company));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Description));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Period));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Title));
            foreach(var item in resume.Positions)
            {
                Console.WriteLine(item.Period + " " + item.Company + " " + item.Title + " " + item.Description);
            }

            Assert.IsTrue(resume.Educations.Count == 2, resume.Educations.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Certificate));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Institute));
            foreach (var item in resume.Educations)
            {
                Console.WriteLine(item.Certificate + " " + item.Institute);
            }

            Assert.IsFalse(string.IsNullOrEmpty(resume.Specialties));
            Console.WriteLine(resume.Specialties);

            Assert.IsFalse(string.IsNullOrEmpty(resume.Summary));
            Console.WriteLine(resume.Summary);
        }

        [Test]
        public void ShouldGetResumeFromPageSource2()
        {
            var resumeSniffer = new LinkedInResumeSniffer("Adam Kahtava", TestHelper.PageSource2);

            var resume = resumeSniffer.GetResume();

            Assert.IsNotNull(resume);

            Assert.IsTrue(resume.Positions.Count == 7, resume.Positions.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Company));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Description));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Period));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Title));
            foreach (var item in resume.Positions)
            {
                Console.WriteLine(item.Period + " " + item.Company + " " + item.Title + " " + item.Description);
            }

            Assert.IsTrue(resume.Educations.Count == 1, resume.Educations.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Certificate));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Institute));
            foreach (var item in resume.Educations)
            {
                Console.WriteLine(item.Certificate + " " + item.Institute);
            }

            Assert.IsFalse(string.IsNullOrEmpty(resume.Specialties));
            Console.WriteLine(resume.Specialties);

            Assert.IsFalse(string.IsNullOrEmpty(resume.Summary));
            Console.WriteLine(resume.Summary);
        }

        [Test]
        public void ShouldGetResumeFromPageSource3()
        {
            var resumeSniffer = new LinkedInResumeSniffer("Adam Kahtava", TestHelper.PageSource3);

            var resume = resumeSniffer.GetResume();

            Assert.IsNotNull(resume);

            Assert.IsTrue(resume.Positions.Count == 2, resume.Positions.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Company));
            Assert.IsTrue(string.IsNullOrEmpty(resume.Positions[1].Description));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Period));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Title));
            foreach (var item in resume.Positions)
            {
                Console.WriteLine(item.Period + " " + item.Company + " " + item.Title + " " + item.Description);
            }

            Assert.IsTrue(resume.Educations.Count == 1, resume.Educations.Count.ToString());
            Assert.IsTrue(string.IsNullOrEmpty(resume.Educations[0].Certificate));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Institute));
            foreach (var item in resume.Educations)
            {
                Console.WriteLine(item.Certificate + " " + item.Institute);
            }

            Assert.IsTrue(string.IsNullOrEmpty(resume.Specialties));
            Console.WriteLine(resume.Specialties);

            Assert.IsTrue(string.IsNullOrEmpty(resume.Summary));
            Console.WriteLine(resume.Summary);
        }

        [Test]
        public void ShouldGetResumeFromPageSource4()
        {
            var resumeSniffer = new LinkedInResumeSniffer("Adam Kahtava", TestHelper.PageSource4);

            var resume = resumeSniffer.GetResume();

            Assert.IsNotNull(resume);

            Assert.IsTrue(resume.Positions.Count == 4, resume.Positions.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Company));
            Assert.IsTrue(string.IsNullOrEmpty(resume.Positions[1].Description));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Period));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[1].Title));
            foreach (var item in resume.Positions)
            {
                Console.WriteLine(item.Period + " " + item.Company + " " + item.Title + " " + item.Description);
            }

            Assert.IsTrue(resume.Educations.Count == 1, resume.Educations.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Certificate));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Institute));
            foreach (var item in resume.Educations)
            {
                Console.WriteLine(item.Certificate + " " + item.Institute);
            }

            Assert.IsFalse(string.IsNullOrEmpty(resume.Specialties));
            Console.WriteLine(resume.Specialties);

            Assert.IsTrue(string.IsNullOrEmpty(resume.Summary));
            Console.WriteLine(resume.Summary);
        }

        [Test]
        public void ShouldGetResumeFromPageSource5()
        {
            var resumeSniffer = new LinkedInResumeSniffer("Adam Kahtava", TestHelper.PageSource5);

            var resume = resumeSniffer.GetResume();

            Assert.IsNotNull(resume);

            Assert.IsTrue(resume.Positions.Count == 5, resume.Positions.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[0].Company));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[0].Description));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[0].Period));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Positions[0].Title));
            foreach (var item in resume.Positions)
            {
                Console.WriteLine(item.Period + " " + item.Company + " " + item.Title + " " + item.Description);
            }

            Assert.IsTrue(resume.Educations.Count == 1, resume.Educations.Count.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Certificate));
            Assert.IsFalse(string.IsNullOrEmpty(resume.Educations[0].Institute));
            foreach (var item in resume.Educations)
            {
                Console.WriteLine(item.Certificate + " " + item.Institute);
            }

            Assert.IsTrue(string.IsNullOrEmpty(resume.Specialties));
            Console.WriteLine(resume.Specialties);

            Assert.IsTrue(string.IsNullOrEmpty(resume.Summary));
            Console.WriteLine(resume.Summary);
        }

    }
}