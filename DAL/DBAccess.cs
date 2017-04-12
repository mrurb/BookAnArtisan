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

		public List<Project> SearchByProjectAddress(string searchparam) // hvor meget behøver vi at hente ud? overvej 1-* relationer. may need multiple sql requests
		{
			List<Project> results = new List<Project>();
            List<User> artisans = new List<User>();
            User client = null;
            string query = "SELECT * FROM projects JOIN Project_status ON Projects.Project_status_ID=project_status.ID JOIN Project_tags ON Project_tags.Project_ID = Projects.ID JOIN Tags ON Tags.ID = Project_tags.Tag_ID JOIN Project_users ON Project_users.Project_ID = Projects.ID JOIN Users ON Project_users.Users_ID = Users.ID WHERE	street_name LIKE '%@address%' OR name LIKE '%@name%' OR project_status.name LIKE '%@status%';";

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
                            //make user modals and loop over them // ud a while loop????? hmm nej måske ikke.. hvordan ser mine rækker ud? check joins .......
                            client = new User(datareader["Users.ID"].ToString(), datareader["Users.First_Name"].ToString(), datareader["Users.Last_Name"].ToString(), datareader["Users.Email"].ToString(), datareader["Users.Password"].ToString(), datareader["Users.Phone"].ToString(), datareader["Users.Address"].ToString());
                            // are you a client or an artisan??? ^
                            //create the artisans also.... v
                            foreach (User user in users)        //need this?
                            {
                                if (user.companyname != null)
                                {
                                    artisans.Add(user);
                                }
                                else
                                {
                                    client = user;
                                }
                            }
                            results.Add(new Project((string)datareader["id"], null, (string)datareader["description"], client, artisans, (string)datareader["address"]));
                            results = null;
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
