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

        public IList<Project> search_by_tag(string search_tag)
        {
            return pc.search_by_tag(search_tag);
        }

        public IList<Project> search_by_project_address(string address)
        {
            return pc.search_by_address(address);
        }

        public IList<Project> search_by_project_name(string pname)
        {
            return pc.search_by_project_name(pname);
        }

        public IList<Project> search_by_project_status(bool status)
        {
            return pc.search_by_project_status(status);
        }

        public IList<Project> search_by_project_user(User user)
        {
            return pc.search_by_project_user(user);
        }
    }
}
