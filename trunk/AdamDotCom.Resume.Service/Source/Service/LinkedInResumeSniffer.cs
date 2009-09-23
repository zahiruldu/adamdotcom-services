using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using AdamDotCom.Resume.Service.Utilities;

namespace AdamDotCom.Resume.Service
{
    public class LinkedInResumeSniffer
    {
        protected static string loginPageUri = "https://www.linkedin.com/secure/login";
        protected static string profilePageUri = "http://www.linkedin.com/profile?viewProfile=&key={0}";
        protected static string searchPageUri = "http://www.linkedin.com/search?search=&lname={1}&fname={0}";

        protected string firstname;
        protected string lastname;
        protected string pageSource;

        private bool isSignedIn = false;
        private WebClient webClient;

        public LinkedInResumeSniffer()
        {
            Errors = new List<KeyValuePair<string, string>>();
        }

        //Overload for testing
        public LinkedInResumeSniffer(string firstnameLastname, string pageSource)
        {
            this.pageSource = pageSource;

            Errors = new List<KeyValuePair<string, string>>();

            SetAndValidateNames(firstnameLastname);

            isSignedIn = true;
        }

        public LinkedInResumeSniffer(string linkedInEmailAddress, string linkedInPassword, string firstnameLastname)
        {
            Errors = new List<KeyValuePair<string, string>>();

            SetAndValidateNames(firstnameLastname);

            if (Errors.Count == 0)
            {
                if (!isSignedIn)
                {
                    webClient = SignIn(linkedInEmailAddress, linkedInPassword);

                    isSignedIn = true;
                }

                try
                {
                    //Open search page, search for user by firstname and lastname
                    webClient.Headers.Remove("Content-Type");
                    string response = webClient.DownloadString(string.Format(searchPageUri, firstname, lastname));

                    //Find the first profile id from search results
                    var profileIdRegex = new Regex("viewProfile=&key=(?<ProfileId>[^&]+)");
                    string profileId = RegexUtilities.GetTokenString(profileIdRegex.Match(response), "ProfileId");
                    if (profileId == null)
                    {
                        AddCriticalError(string.Format("Profile for {0} {1} could not be found", firstname, lastname));
                    }

                    //Open resume page and set the page source
                    pageSource = webClient.DownloadString(string.Format(profilePageUri, profileId));
                }
                catch (Exception ex)
                {
                    AddCriticalError(ex.Message);
                }
            }
        }

        public List<KeyValuePair<string, string>> Errors { get; set; }

        public Resume GetResume()
        {
            var summary = StringUtilities.Scrub(GetSummary());

            var specialties = StringUtilities.Scrub(GetSpecialties());

            var positionPeriods = StringUtilities.Scrub(GetPositionPeriods());
            var positionTitles = StringUtilities.Scrub(GetPositionTitles());
            var positionCompanies = StringUtilities.Scrub(GetPositionCompanies());
            var positionDescriptions = StringUtilities.Scrub(GetPositionDescriptions());

            var positions = positionPeriods.Select(p => new Position {Period = p}).ToList();
            for (int i = 0; i < positions.Count(); i++)
            {
                positions[i].Title = positionTitles == null || positionTitles.Count <= i ? null : positionTitles[i];
                positions[i].Company = positionCompanies == null || positionCompanies.Count <= i ? null : positionCompanies[i];
                positions[i].Description = positionDescriptions == null || positionDescriptions.Count <= i ? null : positionDescriptions[i];
            }

            var educationInstitutes = StringUtilities.Scrub(GetEducationInstitutes());
            var educationCertificates = StringUtilities.Scrub(GetEducationCertificates());

            var educations = educationInstitutes.Select(i => new Education{Institute = i}).ToList();
            for(var i=0 ; i < educations.Count() ; i++)
            {
                educations[i].Certificate = educationCertificates == null || educationCertificates.Count <= i ? null : educationCertificates[i];
            }
 
            return new Resume
                       {
                           Summary = summary, 
                           Specialties = specialties,
                           Educations = educations,
                           Positions = positions
                       };
        }

        public string GetSummary()
        {
            var summaryRegex = new Regex(@"id=""profile-summary(?:[\s\w><=""/]+)p(?:[\s\w><=""']+)>(?<Summary>([^<]+))", RegexOptions.Multiline);
            var summary = RegexUtilities.GetTokenString(summaryRegex.Match(pageSource), "Summary");
            if (string.IsNullOrEmpty(summary))
            {
                AddWarningError("Summary");
            }
            return summary;
        }

        public string GetSpecialties()
        {
            var specialtiesRegex = new Regex(@"id=""profile-specialties(?:[\s\w><=""/]+)p(?:[\s\w><=""']+)>(?<Specialties>(([^<]|<[^/]|</[^p])*.{0,2}))</p", RegexOptions.Multiline);
            var specialties = RegexUtilities.GetTokenString(specialtiesRegex.Match(pageSource), "Specialties");
            if (string.IsNullOrEmpty(specialties))
            {
                AddWarningError("Specialties");
            }
            return specialties;
        }

        public List<string> GetPositionTitles()
        {
            var positionTitlesRegex = new Regex(@"class=""position-title(?:[\s\w><=""/\?&%\+\-:\.]+)name=""title(?:[^>]+)>(?<PositionTitle>(([^<]|<[^/]|</[^a])*.{0,2}))</a", RegexOptions.Multiline);
            var positionTitles = RegexUtilities.GetTokenStringList(positionTitlesRegex.Match(pageSource), "PositionTitle");
            if (positionTitles == null || positionTitles.Count == 0)
            {
                AddWarningError("Position.Titles");
            }
            return positionTitles;
        }

        public List<string> GetPositionCompanies()
        {
            var positionCompaniesRegex = new Regex(@"class=""postitle(?:[\s\w><=""/\?&%\+\-:\.]+)(class=""company-profile""><span>|name=""company""(?:[^>]+)>)(?<Company>([^<]+))", RegexOptions.Multiline);
            var positionCompanies = RegexUtilities.GetTokenStringList(positionCompaniesRegex.Match(pageSource), "Company");
            if (positionCompanies == null || positionCompanies.Count == 0)
            {
                AddWarningError("Position.Companies");
            }
            return positionCompanies;
        }

        public List<string> GetPositionPeriods()
        {
            var positionPeriodsRegex = new Regex(@"class=""postitle(?:[\s\w><\=""'/\?&%\+\-:\.;\(\)]+)class=""period(?:[^>]+)>(?<Period>(([^<]|<[^/]|</[^p])*.{0,2}))</p", RegexOptions.Multiline);
            var positionPeriods = RegexUtilities.GetTokenStringList(positionPeriodsRegex.Match(pageSource), "Period");
            if (positionPeriods == null || positionPeriods.Count == 0)
            {
                AddWarningError("Position.Periods");
            }
            return positionPeriods;
        }

        public List<string> GetPositionDescriptions()
        {
            var positionDescsRegex = new Regex(@"class=""postitle(?:[\s\w><=""/\-\?&%:\+\.;\(\)\#]+)desc""(?:[^>]+)*>(?<Description>(([^<]|<[^/]|</[^p])*.{0,2}))</p", RegexOptions.Multiline);
            var positionDescs = RegexUtilities.GetTokenStringList(positionDescsRegex.Match(pageSource), "Description");
            if (positionDescs == null || positionDescs.Count == 0)
            {
                AddWarningError("Position.Descriptions");
            }
            return positionDescs;
        }

        public List<string> GetEducationInstitutes()
        {
            var educationInstitutesRegex = new Regex(@"name=""eduEntry(?:[\s\w><=""/\-\?&%]+)href(?:[^>]+)>(?<EducationInstitute>([^<]+))", RegexOptions.Multiline);
            var educationInstitutes = RegexUtilities.GetTokenStringList(educationInstitutesRegex.Match(pageSource), "EducationInstitute");
            if (educationInstitutes == null || educationInstitutes.Count == 0)
            {
                AddWarningError("Education.Institutes");
            }
            return educationInstitutes;
        }

        public List<string> GetEducationCertificates()
        {
            var educationCertificateRegex = new Regex(@"name=""eduEntry(?:[\s\w><=""/\-\?&%\+\*]+)h4>(?<EducationCertificate>([^<]+))<(?:[\s\w><=""/\-\?\+&%,]+)href(?:[^>]+)>(?<EducationCertificate>([^<]+))", RegexOptions.Multiline);
            var educationCertificates = RegexUtilities.GetTokenStringList(educationCertificateRegex.Match(pageSource), "EducationCertificate");
            if (educationCertificates == null || educationCertificates.Count == 0)
            {
                AddWarningError("Education.Certificates");
            }
            return educationCertificates;
        }


        private void AddWarningError(string token)
        {
            Errors.Add(new KeyValuePair<string, string>(string.Format("{0}:{1}", "Warning", Errors.Count),
                                                        string.Format("{0} could not be found", token)));
        }

        private void AddCriticalError(string message)
        {
            Errors.Add(new KeyValuePair<string, string>(string.Format("{0}:{1}", "Critical", Errors.Count), message));   
        }

        private void SetAndValidateNames(string firstnameLastname)
        {
            string[] nameSplit = firstnameLastname.Split(new[] {' '}, 2);
            firstname = nameSplit[0];
            lastname = nameSplit[1];
            if (string.IsNullOrEmpty(firstname))
            {
                AddCriticalError("Firstname was not specified");
            }
            if (string.IsNullOrEmpty(lastname))
            {
                AddCriticalError("Lastname was not specified");
            }
        }

        private WebClient SignIn(string linkedInEmailAddress, string linkedInPassword)
        {
            webClient = new WebClientWithCookies();

            try
            {
                //Open the login page
                webClient.DownloadData(loginPageUri);

                //Find the sessionid for the login page
                var sessionIdRegex = new Regex("JSESSIONID=(?<SessionId>[^;]+)");
                string sessionId = RegexUtilities.GetTokenString(sessionIdRegex.Match(webClient.ResponseHeaders["Set-Cookie"]), "SessionId");
                if (sessionId == null)  
                {
                    AddCriticalError("SessionId could not be found");
                }

                //Sign into the login page
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] response = webClient.UploadData(loginPageUri, "POST",
                                                       Encoding.UTF8.GetBytes(
                                                           "csrfToken=" + sessionId +
                                                           "&session_key=" + HttpUtility.UrlEncode(linkedInEmailAddress) +
                                                           "&session_login=Sign In" +
                                                           "&session_login=" +
                                                           "&session_password=" + HttpUtility.UrlEncode(linkedInPassword) +
                                                           "&session_rikey="));

                //Check that we have logged in
                string result = Encoding.ASCII.GetString(response);
                if (!result.Contains("Redirecting..."))
                {
                    AddCriticalError("SignIn failed");
                }
            }
            catch (Exception ex)
            {
                AddCriticalError(ex.Message);
            }
            return webClient;
        }
    }
}