using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using AdamDotCom.Common.Service.Utilities;

namespace AdamDotCom.OpenSource.Service
{
    public class GoogleCodeWebSniffer
    {
        private const string profileLookupUri = "http://code.google.com/u/{0}/";
        private const string commitsLookupUri = "http://code.google.com/feeds/p/{0}/updates/basic";

        public List<KeyValuePair<string, string>> Errors { get; set; }
        public List<Project> Projects { get; set; }

        public GoogleCodeWebSniffer()
        {
            Errors = new List<KeyValuePair<string, string>>();
            Projects = new List<Project>();
        }

        public GoogleCodeWebSniffer(string username)
        {
            Errors = new List<KeyValuePair<string, string>>();

            Projects = new List<Project>();

            var webClient = new WebClient();

            try
            {
                var pageSource = webClient.DownloadString(string.Format(profileLookupUri, username));
                Projects = GetProjects(pageSource);
                Projects = GetProjectDetails(Projects);
                Projects = Clean(Projects);
            }
            catch (Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("GoogleCodeWebSniffer", string.Format("Username {0} not found. Error: {1}", username, ex.Message)));
            }
        }

        private void AddError(string message)
        {
            Errors.Add(new KeyValuePair<string, string>(message, string.Format("Error {0} ", message)));
        }

        public List<Project> GetProjects(string pageSource)
        {
            var projects = new List<Project>();

            var projectMarkupRegex = new Regex(@"Owner&nbsp;role:</th>(?:[\s\w><=""']+)>(?<Projects>(([^<]|<[^/]|</[^t])*.{0,2}))</t");
            var projectMarkupSnippet = RegexUtilities.GetTokenString(projectMarkupRegex.Match(pageSource), "Projects");

            var projectsRegex = new Regex(@"href=""(?<Project>(([^""])*.{0,1}))""", RegexOptions.Multiline);
            var rawProjects = RegexUtilities.GetTokenStringList(projectsRegex.Match(projectMarkupSnippet), "Project");
            
            if (rawProjects == null || rawProjects.Count == 0)
            {
                AddError("No projects were found");
                return null;
            }

            foreach (string project in rawProjects)
            {
                if (!string.IsNullOrEmpty(project))
                {
                    projects.Add(new Project {Name = project, Url = string.Format("http://code.google.com{0}", project)});
                }
            }

            return projects;
        }

        public List<Project> GetProjectDetails(List<Project> projects)
        {
            var webClient = new WebClient();

            foreach (var project in projects)
            {
                try
                {
                    var pageSource = webClient.DownloadString(project.Url);
                    GetProjectDetail(project, pageSource);

                    pageSource = webClient.DownloadString(string.Format(commitsLookupUri, project.Name));
                    GetProjectLastModifedDate(project, pageSource);
                }
                // I don't care about errors
                catch 
                {
                }
            }

            return projects;
        }

        public Project GetProjectDetail(Project project, string pageSource)
        {
            var projectDecriptionRegex = new Regex(@"<a id=""project_summary_link""([^>]*)>(?<ProjectDescription>(([^<]|<[^/]|</[^a])*.{0,2}))</a");
            var projectDescription = RegexUtilities.GetTokenString(projectDecriptionRegex.Match(pageSource), "ProjectDescription");

            if(!string.IsNullOrEmpty(projectDescription))
            {
                project.Description = projectDescription;
            }

            return project;
        }

        public Project GetProjectLastModifedDate(Project project, string xmlSource)
        {
            var document = new XmlDocument();
            document.LoadXml(xmlSource);

            var feed = document["feed"];

            project.LastModified = feed["updated"].InnerText;
            project.LastMessage = feed["entry"]["content"].InnerText;

            return project;
        }

        public List<Project> Clean(List<Project> projects)
        {
            foreach (var project in projects)
            {
                project.Name = CleanName(project.Name);
                project.Url = CleanUrl(project.Url);
                project.LastMessage = CleanCommitMessage(project.LastMessage);
            }

            return projects;
        }

        public string CleanCommitMessage(string lastMessage)
        {
            if( string.IsNullOrEmpty(lastMessage))
            {
                return null;
            }
            var cleanCommitMessage = new Regex(@"&gt;(?<LastCommit>(([^&]|<[^l]|</[^t])*.{0,2}))&lt");
            return RegexUtilities.GetTokenString(cleanCommitMessage.Match(lastMessage),"LastCommit");
        }

        public string CleanUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            return url.Remove(url.LastIndexOf("/"), 1);
        }

        public string CleanName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return name.Replace("/p/", "").Replace("/", "");           
        }
    }
}