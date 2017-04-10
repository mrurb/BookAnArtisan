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
            if(user is Client)
            {
                return dba.SearchByProjectClient(user);
            }
            else
            {
                return dba.SearchByProjectArtisan(user);
            }
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
