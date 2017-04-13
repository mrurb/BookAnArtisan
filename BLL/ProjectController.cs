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


        public List<Project> SearchByProjectUser(User user)
        {
            return dba.SearchByProjectUser(user);
        }

        public List<Project> SearchByProjectAddress(string address)
        {
            return dba.SearchByProjectAddress(address);
        }
    }
}
