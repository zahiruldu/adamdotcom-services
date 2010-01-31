using System;
using System.Collections.Generic;
using System.Net;
using AdamDotCom.Common.Service.Infrastructure;

namespace AdamDotCom.OpenSource.Service
{
    public class OpenSourceService : IOpenSource
    {

        public List<Project> GetProjectsByUsernameXml(string projectHost, string username)
        {
            return GetProjectsByUsername(projectHost, username);
        }

        public List<Project> GetProjectsByUsernameJson(string projectHost, string username)
        {
            return GetProjectsByUsername(projectHost, username);
        }

        private static List<Project> GetProjectsByUsername(string host, string username)
        {
            AssertValidInput(username, "username");          
            AssertValidInput(host, "host");
            var projectHost = GetProjectHost(host);
            AssertValidInput(projectHost.ToString(), "host");

            var projects = new List<Project>().GetFromCache(projectHost, username);
            if (projects != null)
            {
                return projects;
            }

            switch (projectHost)
            {
                case ProjectHost.GoogleCode:
                    projects = GoogleCodeProjectsByUsername(username);
                    break;
                case ProjectHost.GitHub:
                    projects = GitHubProjectsByUsername(username);
                    break;
            }

            return projects.AddToCache(projectHost, username);
        }

        private static List<Project> GitHubProjectsByUsername(string username)
        {
            var translator = new GitHubServiceTranslator(username);
            HandleErrors(translator.Errors);
            return translator.Projects;
        }

        private static List<Project> GoogleCodeProjectsByUsername(string username)
        {
            var sniffer = new GoogleCodeWebSniffer(username);
            HandleErrors(sniffer.Errors);
            return sniffer.Projects;
        }

        private static ProjectHost GetProjectHost(string host)
        {
            ProjectHost projectHost;
            try
            {
                projectHost = (ProjectHost)Enum.Parse(typeof(ProjectHost), host, true);
            }
            catch
            {
                projectHost = ProjectHost.Unknown;
            }
            return projectHost;
        }

        private static void AssertValidInput(string inputValue, string inputName)
        {
            inputName = (string.IsNullOrEmpty(inputName) ? "Unknown" : inputName);

            if (string.IsNullOrEmpty(inputValue) || inputValue.Equals("null", StringComparison.CurrentCultureIgnoreCase) || inputValue.Equals(ProjectHost.Unknown.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new RestException(new KeyValuePair<string, string>(inputName, string.Format("{0} is not a valid value for input {1}.", inputValue, inputName)));
            }
        }

        private static void HandleErrors(List<KeyValuePair<string, string>> errors)
        {
            if(errors != null && errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, errors, (int)ErrorCode.InternalError);
            }
        }
    }
}