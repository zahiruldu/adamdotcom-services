using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace AdamDotCom.OpenSource.Service
{
    public class GitHubServiceTranslator
    {
        private string userLookupUri = "http://github.com/api/v1/xml/{0}";
        private string commitsLookupUri = "http://github.com/api/v1/xml/{0}/{1}/commits/master";

        public List<KeyValuePair<string, string>> Errors { get; set; }
        public List<Project> Projects { get; set; }

        public GitHubServiceTranslator()
        {
            Errors = new List<KeyValuePair<string, string>>();
            Projects = new List<Project>();
        }

        public GitHubServiceTranslator(string username)
        {
            Errors = new List<KeyValuePair<string, string>>();
            Projects = new List<Project>();

            var webClient = new WebClient();

            try
            {
                var xmlSource = webClient.DownloadString(string.Format(userLookupUri, username));
                Projects = GetProjects(xmlSource);
                Projects = GetProjectDetails(Projects, username);
                Projects = Clean(Projects);
            }
            catch (Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("GitHubServiceTranslator", string.Format("Username {0} not found. Error: {1}", username, ex.Message)));
            }
        }

        private List<Project> Clean(List<Project> projects)
        {
            foreach (var project in projects)
            {
                project.LastModified = CleanLastModified(project.LastModified);
            }
            return projects;
        }

        private string CleanLastModified(string lastModified)
        {
            if (string.IsNullOrEmpty(lastModified))
            {
                return null;
            }
            return lastModified.Split('T')[0];
        }


        public List<Project> GetProjects(string xmlSource)
        {
            var projects = new List<Project>();
            var document = new XmlDocument();

            document.LoadXml(xmlSource);
            foreach (XmlElement repository in document["user"]["repositories"])
            {   
                projects.Add(new Project
                                 {
                                     Name = repository["name"].InnerText,
                                     Url = repository["url"].InnerText,
                                     Description = repository["description"].InnerText
                                 });
            }

            return projects;
        }

        public List<Project> GetProjectDetails(List<Project> projects, string username)
        {
            var webClient = new WebClient();

            foreach (var project in projects)
            {
                try
                {
                    var pageSource = webClient.DownloadString(string.Format(commitsLookupUri, username, project.Name));
                    GetProjectDetail(project, pageSource);
                }
                // I don't care about errors
                catch
                {
                }
            }

            return projects;
        }

        public Project GetProjectDetail(Project project, string xmlSource)
        {
            var document = new XmlDocument();
            document.LoadXml(xmlSource);

            var commit = document["commits"]["commit"];

            project.LastModified = commit.SelectNodes("committed-date")[0].InnerText;
            project.LastMessage = commit.SelectNodes("message")[0].InnerText;

            return project;
        }
    }
}