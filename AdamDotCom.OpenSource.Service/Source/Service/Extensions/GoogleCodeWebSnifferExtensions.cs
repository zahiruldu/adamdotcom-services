using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using AdamDotCom.Common.Service.Utilities;

namespace AdamDotCom.OpenSource.Service
{
    public static class GoogleCodeWebSnifferExtensions
    {
        public static List<Project> ParseProjects(this List<Project> projects, string pageSource)
        {            
            var projectsRegex = new Regex(@"name=""owner([\s\w><=""']*)href=""(?<Project>(([^""])*.{0,1}))""", RegexOptions.Multiline);            
            var rawProjects = RegexUtilities.GetTokenStringList(projectsRegex.Match(pageSource), "Project");

            if (rawProjects == null || rawProjects.Count == 0)
            {
                throw new Exception("Projects could not be parsed. No projects were returned.");
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
        
        public static Project ParseDetails(this Project project, string pageSource)
        {
            var projectDecriptionRegex = new Regex(@"<a id=""project_summary_link""([^>]*)>(?<ProjectDescription>(([^<]|<[^/]|</[^a])*.{0,2}))</a");
            var projectDescription = RegexUtilities.GetTokenString(projectDecriptionRegex.Match(pageSource), "ProjectDescription");

            if(!string.IsNullOrEmpty(projectDescription))
            {
                project.Description = projectDescription;
            }

            return project;
        }
        
        public static Project ParseLastModifiedDate(this Project project, string xmlSource)
        {
            var document = new XmlDocument();
            document.LoadXml(xmlSource);

            var feed = document["feed"];

            project.LastModified = feed["updated"].InnerText;
            project.LastMessage = feed["entry"]["content"].InnerText;

            return project;
        }
        
        public static List<Project> Clean(this List<Project> projects)
        {
            foreach (var project in projects)
            {
                project.Name = project.Name.CleanName();
                project.Url = project.Url.CleanUrl();
                project.LastMessage = project.LastMessage.CleanCommitMessage();
                project.LastModified = project.LastModified.CleanLastModified();
            }

            return projects;
        }

        public static string CleanName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return name.Replace("/p/", "").Replace("/", "");           
        }
        
        public static string CleanUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            return url.Remove(url.LastIndexOf("/"), 1);
        }

        public static string CleanLastModified(this string lastModified)
        {
            if (string.IsNullOrEmpty(lastModified))
            {
                return null;
            }
            return lastModified.Split('T')[0];
        }

        public static string CleanCommitMessage(this string lastMessage)
        {
            if (string.IsNullOrEmpty(lastMessage))
            {
                return null;
            }
            var cleanCommitMessage = new Regex(@""">(?<LastCommit>(([^<])*.{0,1}))<");
            var result = RegexUtilities.GetTokenString(cleanCommitMessage.Match(lastMessage),"LastCommit");

            return result;
        }
    }
}