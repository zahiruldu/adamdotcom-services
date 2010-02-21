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

            try
            {
                var isRemovingDuplicateItems = false;
                if (filters.Has("remove:duplicate-items"))
                {
                    isRemovingDuplicateItems = true;
                    filters.Replace("remove:duplicate-items", "remove:-");
                }

                var filterSplit = filters.Split(',');
                foreach (var filterToken in filterSplit)
                {
                    if (filterToken.Has("remove:"))
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
                }

                if (isRemovingDuplicateItems)
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
            }
            catch
            {
                return projects;
            }

            return projects;
        }
    }
}