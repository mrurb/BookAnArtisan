using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;

namespace DAL
{
    public class DBAccess
    {

        //chapter 21

        public List<Project> SearchByProjectArtisan(User user)
        {
            throw new NotImplementedException();
        }

        public List<Project> SearchByTag(string search_tag)
        {
            throw new NotImplementedException();
        }

        public List<Project> SearchByProjectClient(User user)
        {
            throw new NotImplementedException();
        }

        public List<Project> SearchByProjectAddress(string address)
        {
            List<Project> results = new List<Project>();
            string query = "SELECT * FROM projects WHERE address = @address";
            SqlParameter sqlparams = new SqlParameter("@address", System.Data.SqlDbType.NVarChar);
            sqlparams.Value = address;
            using (SqlConnection con = new SqlConnection("CONNECTIONSTRING GOES HERE"))                     // TODO fix (find in config)
            {
                SqlCommand sqlcmd = new SqlCommand(query, con);
                try
                {
                    SqlDataReader datareader = sqlcmd.ExecuteReader();
                    while (datareader.Read())
                    {
                        results.Add(new Project(datareader["id"], datareader["tag"], datareader["description"], datareader["client"], datareader["artisan"], datareader["address"]));
                    }
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            // create connection

            //return result
            return null;
        }

        public List<Project> SearchByProjectStatus(bool status)
        {
            throw new NotImplementedException();
        }

        public List<Project> SearchByProjectName(string pname)
        {
            throw new NotImplementedException();
        }
    }
}
