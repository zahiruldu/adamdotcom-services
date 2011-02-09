using System;
using System.Collections.Generic;
using System.Net;
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
                Projects = Projects.ParseProjects(pageSource);
                 
                for (var i = 0 ; i < Projects.Count ; i++)
                {
                    var project = Projects[i];
                    
                    pageSource = webClient.DownloadString(project.Url);
                    project = project.ParseDetails(pageSource);

                    pageSource = webClient.DownloadString(string.Format(commitsLookupUri, project.Name.CleanName()));
                    project = project.ParseLastModifiedDate(pageSource);
                }

                Projects = GoogleCodeWebSnifferExtensions.Clean(Projects);
            }
            catch (Exception ex)
            {
                Errors.Add(new KeyValuePair<string, string>("GoogleCodeWebSniffer", string.Format("An error occured when looking up {0}. Error details: {1}", username, ex.Message)));
            }
        }
    }
}