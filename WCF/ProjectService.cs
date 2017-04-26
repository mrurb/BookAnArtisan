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
        public Project Create(Project t)
        {
            return projectController.Create(t);
        }

        public Project Read(Project project)
        {
            return projectController.Read(project);
        }

        public Project Update(Project t)
        {
            return projectController.Update(t);
        }

        public bool Delete(Project t)
        {
            return projectController.Delete(t);
        }

        public List<Project> ReadAll()
        {
            return projectController.ReadAll();
        }
    }
}