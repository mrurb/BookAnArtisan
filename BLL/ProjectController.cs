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
        ProjectDB db = new ProjectDB();
        SearchDB dba = new SearchDB();
		UserController uctr = new UserController();
        public Project Create(Project project)
        {
			var a = uctr.Read(project.Contact);
	        if (a.UserName != project.Contact.UserName)
	        {
		        throw new ApplicationException("User: Contact invalid.");
	        }
	        var b = uctr.Read(project.CreatedBy);
	        if (b.UserName != project.CreatedBy.UserName)
	        {
		        throw new ApplicationException("User: CreatedBy invalid.");
	        }
			return db.Create(project);
        }

        public Project Read(Project project)
        {
            return db.Read(project);
        }

        public Project Update(Project project)
        {
            return db.Update(project);
        }

        public Project Delete(Project project)
        {
            return db.Delete(project);
        }

        public List<Project> ReadAll()
        {
            return db.ReadAll();
        }


        public List<Project> ReadAllForUser(User user)
        {
            return db.ReadAllForUser(user);
        }
        /*
        public List<Project> SearchByTag(string search_tag)
        {
            return dba.SearchByTag(search_tag);
        }*/

        public List<Project> SearchByProjectAddress(Project p)
        {
            return dba.SearchByProjectAddress(p);
        }
    }
}

