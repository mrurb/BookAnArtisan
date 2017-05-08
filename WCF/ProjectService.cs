using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace WCF
{
    public class ProjectService : IProjectService
    {
        ProjectController projectController = new ProjectController();
        public Project CreateProject(Project t)
        {
            return projectController.Create(t);
        }

        public Project ReadProject(Project project)
        {
            return projectController.Read(project);
        }

        public Project UpdateProject(Project t)
        {
            return projectController.Update(t);
        }

        public Project DeleteProject(Project t)
        {
            return projectController.Delete(t);
        }

        public List<Project> ReadAllProject()
        {
            return projectController.ReadAll();
        }

        public List<Project> ReadAllProjectsForUser(User user)
        {
            return projectController.ReadAllForUser(user);
        }
    }
}