﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class ProjectController : IController<Project>
    {
        public Project Create(Project project)
        {
            ProjectDB db = new ProjectDB();
            return db.Create(project);
        }

        public Project Read(Project project)
        {
            ProjectDB db = new ProjectDB();
            return db.Read(project);
        }

        public Project Update(Project project)
        {
            ProjectDB db = new ProjectDB();
            return db.Update(project);
        }

        public Project Delete(Project project)
        {
            ProjectDB db = new ProjectDB();
            return db.Delete(project);
        }

        public List<Project> ReadAll()
        {
            ProjectDB db = new ProjectDB();
            return db.ReadAll();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class ProjectController
    {
        DBAccess dba = new DBAccess();
        public List<Project> SearchByTag(string search_tag)
        {
            return dba.SearchByTag(search_tag);
        }

        public List<Project> SearchByProjectStatus(bool status)
        {
            return dba.SearchByProjectStatus(status);
        }

        public List<Project> SearchByProjectUser(User user)
        {
            return dba.SearchByProjectUser(user);
        }

        public List<Project> SearchByProjectAddress(string address)
        {
            return dba.SearchByProjectAddress(address);
        }

        public List<Project> SearchByProjectName(string pname)
        {
            return dba.SearchByProjectName(pname);
        }
    }
}
