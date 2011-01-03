using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace AdamDotCom.OpenSource.Service
{
    public static class GitHubServiceTranslatorExtensions
    {
        private static List<Project> Clean(this List<Project> projects)
        {
            foreach (var project in projects)
            {
                project.LastModified = CleanLastModified(project.LastModified);
            }
            return projects;
        }

        private static string CleanLastModified(this string lastModified)
        {
            if (string.IsNullOrEmpty(lastModified))
            {
                return null;
            }
            return lastModified.Split('T')[0];
        }


        public static List<Project> GetProjects(this List<Project> projects, string xmlSource)
        {
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

        public static Project GetDetails(this Project project, string xmlSource)
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