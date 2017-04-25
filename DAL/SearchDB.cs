using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DAL
{
	public class SearchDB
	{
        static string connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
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

        /*
         * A function that finds a set of projects based on a search term of tags.
         * @params search_tag a string search term searching for tags
         * @returns projects a list of projects of which contains the tag searched for
         */
		public List<Project> SearchByTag(string search_tag)
		{
            List<Project> results = new List<Project>();
            List<User> artisans = new List<User>();
            User client = null;
            List<string> tags = new List<string>();
            tags.Add(search_tag);
            string query = "SELECT * FROM projects JOIN Project_tags ON Projects.ID=project_tags.Project_ID JOIN Tags ON Project_tags.Tag_ID = Tags.ID WHERE Tags.Name LIKE '%@tag%'";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcommand = new SqlCommand(query, con))
                {
                    try
                    {
                        sqlcommand.Parameters.Add(new SqlParameter("@tag", search_tag));
                        con.Open();
                        using (SqlDataReader datareader = sqlcommand.ExecuteReader())
                        {
                            while (datareader.Read())
                            {
                                Project p = new Project(datareader["id"].ToString(), tags, datareader["Project_description"].ToString(), client, artisans, datareader["street_name"].ToString());
                                results.Add(p);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                }
            }
            foreach (Project item in results)
            {
                //AppendTags(item);
            }
            return results;
        }

        /*
         * Private function: append all the tags of a given project by sweeping the database.
         * @params project p
         * @returns null
         */
        private void AppendTags(Project p)
        {
            List<string> results = new List<string>();
            string query = "SELECT * FROM Project_tags JOIN Tags ON (Project_tags.Project_ID=@projectid AND Project_tags.Tag_ID = Tags.ID);";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcommand = new SqlCommand(query, con))
                {
                    try
                    {
                        sqlcommand.Parameters.Add(new SqlParameter("@projectid", p.id));
                        con.Open();
                        SqlDataReader reader = sqlcommand.ExecuteReader();
                        while (reader.Read())
                        {
                            results.Add(reader["Name"].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                }
            }
            p.tags = results;
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
            List<User> artisans = new List<User>();
            User client = null;
            List<string> tags = new List<string>();
            string query = "SELECT * FROM projects JOIN Project_status ON Projects.Project_status_ID=project_status.ID WHERE projects.street_name LIKE '%@address%' OR projects.name LIKE '%@name%' OR project_status.name LIKE '%@status%';";
            SqlParameter addressParameter = new SqlParameter { ParameterName = "@address", SqlValue = searchparam, SqlDbType = SqlDbType.NVarChar };
            SqlParameter nameParameter = new SqlParameter { ParameterName = "@name", SqlValue = searchparam, SqlDbType = SqlDbType.NVarChar };
            SqlParameter statusParameter = new SqlParameter { ParameterName = "@status", SqlValue = searchparam, SqlDbType = SqlDbType.NVarChar };
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcommand = new SqlCommand(query, con))
                {
                    try
                    {
                        sqlcommand.Parameters.Add(addressParameter);
                        sqlcommand.Parameters.Add(nameParameter);
                        sqlcommand.Parameters.Add(statusParameter);
                        con.Open();
                        using (SqlDataReader datareader = sqlcommand.ExecuteReader())
                        {
                            while (datareader.Read())
                            {
                                Project p = new Project(datareader["id"].ToString(), tags, datareader["Project_description"].ToString(), client, artisans, datareader["street_name"].ToString());
                                results.Add(p);
                            }
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
