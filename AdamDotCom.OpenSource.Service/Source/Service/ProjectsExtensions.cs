using System;
using System.Collections.Generic;
using System.Linq;

namespace AdamDotCom.OpenSource.Service
{
    public static class ProjectsExtensions
    {
        public static List<Project> Filter(this List<Project> projects, string filters)
        {
            if(string.IsNullOrEmpty(filters))
            {
                return projects;
            }

            var filterSplit = filters.Split(',');
            foreach (var filterToken in filterSplit)
            {
                if (filterToken.IndexOf("remove-old-duplicates", StringComparison.InvariantCultureIgnoreCase) != -1)
                    continue;
                foreach (var project in projects)
                {
                    if (project.Name.IndexOf(filterToken, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        project.Name = project.Name.Replace(filterToken, "");
                    }
                    project.Name = project.Name.Replace("-", " ").Trim();
                }
            }

            if (filters.IndexOf("remove-old-duplicates", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                var orderedProjects = projects.OrderBy(p => p.Name).ThenByDescending(p => p.LastModified).ToList();
                var projectsToReturn = new List<Project>();
                var lastProjectName = string.Empty;
                for (var i = 0; i < orderedProjects.Count; i++)
                {
                    var thisProjectName = orderedProjects[i].Name;

                    if (lastProjectName != thisProjectName)
                    {
                        projectsToReturn.Add(orderedProjects[i]);
                    }

                    lastProjectName = thisProjectName;
                }

                projects = projectsToReturn;
            }

            return projects;
        }
    }
}
