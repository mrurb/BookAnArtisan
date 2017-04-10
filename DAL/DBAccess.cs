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
						User client = (User)datareader["client"];
						User artisan = (User)datareader["artisan"];                                         // TODO make users generic
																											//List<string> tags = (string)datareader["tag"];                                           // TODO how to retrieve tags? how are they stored?
						List<string> tags = new List<string> {"stuff", "stuff" };
						results.Add(new Project((string)datareader["id"], tags, (string)datareader["description"], client, artisan, (string)datareader["address"]));
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
