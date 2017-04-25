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
        public Project Create(Project t)
        {
            ProjectController projectController = new ProjectController();
            return projectController.Create(t);
        }

        public Project Read(Project project)
        {
            ProjectController projectController = new ProjectController();
            return projectController.Read(project);
        }

        public Project Update(Project t)
        {
            ProjectController projectController = new ProjectController();
            return projectController.Update(t);
        }

        public Project Delete(Project t)
        {
            ProjectController projectController = new ProjectController();
            return projectController.Delete(t);
        }

        public List<Project> ReadAll()
        {
            ProjectController projectController = new ProjectController();
            return projectController.ReadAll();
        }
    }
}