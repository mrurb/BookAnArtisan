using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
	public class DBAccess
	{
        string connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        //chapter 21        TODO API ????

        public bool DBConnectionTest()
        {
            string query = "SELECT * FROM dbo.AspNetUsers WHERE EmailConfirmed = @id";
            string result = "";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcommand = new SqlCommand(query, con))
                {
                    try
                    {
                        sqlcommand.Parameters.Add(new SqlParameter("@id", false));
                        con.Open();
                        SqlDataReader reader = sqlcommand.ExecuteReader();
                        while (reader.Read())
                        {
                            result = reader.GetValue(1).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                }
            }
            return true;
        }
        
		public List<Project> SearchByProjectUser(User user)
		{
			throw new NotImplementedException();
		}

		public List<Project> SearchByTag(string search_tag)
		{
			throw new NotImplementedException();
		}


        /*
         * A function that searches through projects and tries to find a project that
         * matches based on either project name, project address or status of the project
         * Note: the returned projects is a "quick view" only! 
         * There are no tags, artisans or clients appended!
         * @params takes a string as a search parameter
         * @return list of projects
         * @throws generic exception.
         */
		public List<Project> SearchByProjectAddress(string searchparam)
		{
			List<Project> results = new List<Project>();
            List<User> artisans = null;
            User client = null;
            List<string> tags = null;
            string query = "SELECT * FROM projects JOIN Project_status ON Projects.Project_status_ID=project_status.ID WHERE street_name LIKE '%asd%' OR projects.name LIKE '%myproject%' OR project_status.name LIKE '%done%';";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcommand = new SqlCommand(query, con))
                {
                    try
                    {
                        sqlcommand.Parameters.Add(new SqlParameter("@address", searchparam));
                        sqlcommand.Parameters.Add(new SqlParameter("@name", searchparam));
                        sqlcommand.Parameters.Add(new SqlParameter("@status", searchparam));
                        con.Open();
                        SqlDataReader datareader = sqlcommand.ExecuteReader();
                        while (datareader.Read())
                        {
                            Project p = new Project(datareader["id"].ToString(), tags, datareader["Project_description"].ToString(), client, artisans, datareader["street_name"].ToString());
                            int i;
                            results.Add(p);
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                }
            }
			return results;
		}
	}
}
