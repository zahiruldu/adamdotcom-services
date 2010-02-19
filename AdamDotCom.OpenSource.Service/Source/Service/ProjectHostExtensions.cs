using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdamDotCom.Common.Service.Infrastructure;
using AdamDotCom.Common.Service.Utilities;

namespace AdamDotCom.OpenSource.Service
{
    public static class ProjectHostExtensions
    {
        public static List<KeyValuePair<string, string>> ParseProjectHostAndUsername(string projectHostUsernamePair)
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

        public static ProjectHost GetProjectHost(string host)
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
    }
}
