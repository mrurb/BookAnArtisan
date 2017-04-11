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

		public List<Project> SearchByProjectUser(User user)
		{
			throw new NotImplementedException();
		}

		public List<Project> SearchByTag(string search_tag)
		{
			throw new NotImplementedException();
		}

		public List<Project> SearchByProjectAddress(string address)
		{
			List<Project> results = new List<Project>();
            List<User> artisans = new List<User>();
            User client = null;

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
                        List<User> users = GetUserForProject((string)datareader["client"], (string)datareader["artisan"]);
                        foreach (User user in users)
                        {
                            if(user.companyname != null)
                            {
                                artisans.Add(user);
                            }
                            else
                            {
                                client = user;
                            }
                        }
						results.Add(new Project((string)datareader["id"], null, (string)datareader["description"], client, artisans, (string)datareader["address"]));
					}
                    foreach (Project p in results)
                    {
                        List<string> tags = GetTagsForProject(p.id);
                        p.tags = tags;
                    }
                }
				catch (Exception)
				{
					throw new Exception();
				}
			}
			return results;
		}

        private List<string> GetTagsForProject(string id)
        {
            List<string> tags = new List<string>();
            string query = "SELECT * FROM tags WHERE projectid = @id";                                          // check name "projectid"
            SqlParameter sqlparamsclient = new SqlParameter("@id", System.Data.SqlDbType.NVarChar);
            sqlparamsclient.Value = id;
            using(SqlConnection con = new SqlConnection("CONNECTION STRING GOES HERE"))                         // TODO FIX
            {
                SqlCommand sqlcommand = new SqlCommand(query, con);
                try
                {
                    SqlDataReader reader = sqlcommand.ExecuteReader();
                    while (reader.Read())
                    {
                        tags.Add((string)reader["name"]);
                    }
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }

            return tags;
        }

        private List<User> GetUserForProject(string clientid, string artisanid)
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM users WHERE id = @clientid OR id = @artisanid";
            SqlParameter sqlparamsclient = new SqlParameter("@clientid", System.Data.SqlDbType.NVarChar);
            sqlparamsclient.Value = clientid;
            SqlParameter sqlparamsartisan = new SqlParameter("@artisanid", System.Data.SqlDbType.NVarChar);
            sqlparamsartisan.Value = artisanid;
            using(SqlConnection con = new SqlConnection("CONNECTION STRING GOES HERE"))                             // TODO FIX
            {
                SqlCommand sqlcommand = new SqlCommand(query, con);
                try
                {
                    SqlDataReader reader = sqlcommand.ExecuteReader();
                    while (reader.Read())
                    {
                        User u = new User((string)reader["id"], (string)reader["name"], (string)reader["email"], (string)reader["password"], (string)reader["PhoneNumber"], (string)reader["address"]);
                        users.Add(u);
                        if (reader["CompanyName"] != null)
                        {
                            u.companyname = (string)reader["CompanyName"];
                            u.profession = (string)reader["profession"];
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        
            return users;
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
