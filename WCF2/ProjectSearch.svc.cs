using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL;
using Model;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProjectSearch" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProjectSearch.svc or ProjectSearch.svc.cs at the Solution Explorer and start debugging.
    public class ProjectSearch : IProjectSearch
    {
        ProjectController pc = new ProjectController();

        public IList<Project> SearchByTag(string search_tag)
        {
            return pc.SearchByTag(search_tag);
        }

        public IList<Project> SearchByProjectAddress(string address)
        {
            return pc.SearchByProjectAddress(address);
        }

        public IList<Project> SearchByProjectStatus(bool status)
        {
            return pc.SearchByProjectStatus(status);
        }

        public IList<Project> SearchByProjectUser(User user)
        {
            return pc.SearchByProjectUser(user);
        }

        public IList<Project> SearchByProjectName(string pname)
        {
            return pc.SearchByProjectName(pname);
        }
    }
}
