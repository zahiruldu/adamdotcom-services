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
                Projects = Projects.GetProjects(xmlSource);
                
                for (var i = 0 ; i < Projects.Count ; i++)
                {
                    var project = Projects[i];
                    var pageSource = webClient.DownloadString(string.Format(commitsLookupUri, username, project.Name));
                    project = project.GetDetails(pageSource);
                }

                Projects = GitHubServiceTranslatorExtensions.Clean(Projects);
            }
            catch (Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("GitHubServiceTranslator", string.Format("Username {0} not found. Error: {1}", username, ex.Message)));
            }
        }
    }
}