using System;
using System.Collections.Generic;
using System.Linq;

namespace AdamDotCom.OpenSource.Service
{
    public static class ProjectsExtensions
    {
        public static bool Has(this string value, string testValue)
        {
            return value.IndexOf(testValue, StringComparison.InvariantCultureIgnoreCase) != -1;
        }

        public static List<Project> Filter(this List<Project> projects, string filters)
        {
            if(string.IsNullOrEmpty(filters))
            {
                return projects;
            }

            if (!filters.Has("remove:"))
            {
                return projects;
            }

            try
            {
                if (filters.Has("empty-items"))
                {
                    projects = projects.FilterEmptyRepositories();
                }

                var shouldRemoveDuplicateRepositories = false;
                if (filters.Has("duplicate-items"))
                {
                    shouldRemoveDuplicateRepositories = true;
                    filters.Replace("duplicate-items", "-");
                }

                projects = projects.FilterProjectNamesByUserDefinedFilters(filters);

                if (shouldRemoveDuplicateRepositories)
                {
                    projects = projects.FilterDuplicateProjectsByLastModified();
                }
            }
            catch
            {
                return projects;
            }

            return projects;
        }

        private static List<Project> FilterProjectNamesByUserDefinedFilters(this List<Project> projects, string filters)
        {
            var filterSplit = filters.Split(',');
            foreach (var filterToken in filterSplit)
            {
                var stringToRemove = filterToken.Replace("remove:", "");
                foreach (var project in projects)
                {
                    if (project.Name.Has(stringToRemove))
                    {
                        project.Name = project.Name.Replace(stringToRemove, " ").Trim();
                    }
                }
            }

            return projects;
        }

        public static List<Project> FilterDuplicateProjectsByLastModified(this List<Project> projects)
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

            return projectsToReturn;
        }

        public static List<Project> FilterEmptyRepositories(this List<Project> projects)
        {
            var projectsToReturn = new List<Project>();
            foreach (var project in projects)
            {
                if (!string.IsNullOrEmpty(project.LastMessage) || !string.IsNullOrEmpty(project.LastModified))
                {
                    projectsToReturn.Add(project); 
                }
            }
            return projectsToReturn;
        }
    }
}