using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AdamDotCom.Common.Service.Infrastructure;
using AdamDotCom.Common.Service.Utilities;

namespace AdamDotCom.OpenSource.Service
{
    public class OpenSourceService : IOpenSource
    {

        public Projects GetProjectsByUsernameXml(string projectHost, string username)
        {
            return GetProjectsByUsername(projectHost, username);
        }

        public Projects GetProjectsByUsernameJson(string projectHost, string username)
        {
            return GetProjectsByUsername(projectHost, username);
        }

        public Projects GetProjectsByProjectHostAndUsernameXml(string projectHostUsernamePair)
        {
            return GetProjectsByProjectHostAndUsername(projectHostUsernamePair);
        }

        public Projects GetProjectsByProjectHostAndUsernameJson(string projectHostUsernamePair)
        {
            return GetProjectsByProjectHostAndUsername(projectHostUsernamePair);
        }

        private static Projects GetProjectsByUsername(string host, string username)
        {
            AssertValidInput(username, "username");          
            AssertValidInput(host, "host");
            ProjectHost projectHost = GetProjectHost(host);
            AssertValidInput(projectHost.ToString(), "host");

            var projects = new List<Project>().GetFromCache(projectHost, username);
            if (projects != null)
            {
                return new Projects(projects);
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

            return new Projects(projects.AddToCache(projectHost, username));
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

        public Projects GetProjectsByProjectHostAndUsername(string projectHostUsernamePair)
        {
            var hostAndUsername = ParseProjectHostAndUsername(projectHostUsernamePair);

            var projects = new List<Project>();
            foreach (var hostUsername in hostAndUsername)
            {
                projects.AddRange(GetProjectsByUsername(hostUsername.Key, hostUsername.Value));
            }

            return new Projects(projects.OrderBy(p => p.Name).ThenByDescending(p => p.LastModified));
        }

        public List<KeyValuePair<string, string>> ParseProjectHostAndUsername(string projectHostUsernamePair)
        {
            var projectHostsRegex = new Regex(@"(^|,)(?<ProjectHost>(([^:])*.{0,1})):");
            var projects = RegexUtilities.GetTokenStringList(projectHostsRegex.Match(projectHostUsernamePair), "ProjectHost");

            var usernameRegex = new Regex(@":(?<UserName>(([^$|^,])*))");
            var usernames = RegexUtilities.GetTokenStringList(usernameRegex.Match(projectHostUsernamePair), "UserName");

            if (projects == null || usernames == null || projects.Count != usernames.Count)
            {
                throw new RestException(new KeyValuePair<string, string>("project-host:username", string.Format("project-host:username pairs could not be resolved.")));
            }

            var hostAndUsername = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < projects.Count; i++)
            {
                if (projects[i] != null && usernames[i] != null)
                {
                    hostAndUsername.Add(new KeyValuePair<string, string>(projects[i], usernames[i]));
                }
            }

            return hostAndUsername;
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