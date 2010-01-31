using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace AdamDotCom.OpenSource.Service
{
    public class GitHubServiceTranslator
    {
        private string userLookupUri = "http://github.com/api/v1/xml/{0}";

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
            }
            catch (Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("GitHubServiceTranslator", string.Format("Username {0} not found. Error: {1}", username, ex.Message)));
            }
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
    }
}