using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using AdamDotCom.Common.Service.Utilities;

namespace AdamDotCom.OpenSource.Service
{
    public class GoogleCodeWebSniffer
    {
        private const string profileUri = "http://code.google.com/u/{0}/";

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
                var pageSource = webClient.DownloadString(string.Format(profileUri, username));
                Projects = GetProjects(pageSource);
                Projects = GetProjectDetails(Projects);
                Projects = Clean(Projects);
            }
            catch (Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("GoogleCodeWebSniffer", string.Format("Username {0} not found", username)));
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

        public List<Project> Clean(List<Project> projects)
        {
            foreach (var project in projects)
            {
                project.Name = project.Name.Replace("/p/", "").Replace("/", "");
            }

            return projects;
        }
    }
}