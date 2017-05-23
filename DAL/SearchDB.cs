using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
	public class SearchDb
	{
		public readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

		/*
		 * Private function: append all the tags of a given project by sweeping the database.
		 * @params project p
		 * @returns null
		 */
		private void AppendTags(Project p)
		{
			var results = new List<string>();
			const string query =
				"SELECT * FROM Project_tags JOIN Tags ON (Project_tags.Project_ID=@projectid AND Project_tags.Tag_ID = Tags.ID);";
			using (var con = new SqlConnection(ConnectionString))
			{
				using (var sqlcommand = new SqlCommand(query, con))
				{
					try
					{
						sqlcommand.Parameters.Add(new SqlParameter("@projectid", p.Id));
						con.Open();
						var reader = sqlcommand.ExecuteReader();
						while (reader.Read())
							results.Add(reader["Name"].ToString());
					}
					catch (Exception)
					{
						throw new Exception();
					}
				}
			}
			p.Tags = results;
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
		public List<Project> SearchByProjectAddress(Project pInput) //read uncommitted? hurtigere.
		{
			var results = new List<Project>();
			var query =
				"SELECT projects.id, projects.name pname, artisan.FirstName afirstname, client.FirstName cfirstname, Tags.Name tag FROM projects JOIN AspNetUsers client ON projects.Created_by_ID = client.Id JOIN AspNetUsers artisan ON projects.Contact_ID = artisan.id JOIN project_status ON project_status.id = projects.id JOIN Project_tags ON project_tags.Project_ID = projects.ID JOIN Tags ON Tags.ID = project_tags.Tag_ID WHERE street_name LIKE @address OR projects.name LIKE @name OR project_status.name LIKE @status";
			SqlParameter[] arrayofparams =
			{
				new SqlParameter
				{
					ParameterName = "@address",
					SqlValue = "%" + pInput.StreetName + "%",
					SqlDbType = SqlDbType.NVarChar
				},
				new SqlParameter {ParameterName = "@name", SqlValue = "%" + pInput.Name + "%", SqlDbType = SqlDbType.NVarChar},
				new SqlParameter
				{
					ParameterName = "@status",
					SqlValue = "%" + pInput.ProjectStatusID + "%",
					SqlDbType = SqlDbType.NVarChar
				}
			};
			if (pInput.Contact != null)
			{
				query += " OR client.UserName LIKE @contact OR artisan.UserName LIKE @contact";
				arrayofparams[arrayofparams.Length] = new SqlParameter
				{
					ParameterName = "@contact",
					SqlValue = "%" + pInput.Contact + "%",
					SqlDbType = SqlDbType.NVarChar
				};
			}
			if (pInput.Tags != null)
			{
				query += " OR Tags.name LIKE @tag";
				arrayofparams[arrayofparams.Length] = new SqlParameter
				{
					ParameterName = "@tag",
					SqlValue = "%" + pInput.Tags[0] + "%",
					SqlDbType = SqlDbType.NVarChar
				};
			}
			var con = new SqlConnection(ConnectionString);
			var sqlcommand = new SqlCommand(query, con);
			con.Open();
			var myTrans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
			try
			{
				sqlcommand.Parameters.AddRange(arrayofparams);
				sqlcommand.Transaction = myTrans;
				using (var datareader = sqlcommand.ExecuteReader())
				{
					if (datareader.HasRows)
						while (datareader.Read())
						{
							var p = new Project
							{
								Id = (int) datareader["id"],
								CreatedBy = new User {FirstName = datareader["cfirstname"].ToString()},
								Contact = new User {FirstName = datareader["afirstname"].ToString()},
								Name = datareader["pname"].ToString()
							};
							results.Add(p);
							AppendTags(p); //can I do this??
						}
				}
				myTrans.Commit();
			}
			catch (Exception)
			{
				throw new Exception();
			}
			return results;
		}
	}

	#region Unimplemented functions

	/*
	 * A function that finds a set of projects based on a search term of tags.
	 * @params search_tag a string search term searching for tags
	 * @returns projects a list of projects of which contains the tag searched for
	 */
	/*
   public List<Project> SearchByTag(string search_tag)
   {
	   List<Project> results = new List<Project>();
	   List<User> artisans = new List<User>();
	   User client = null;
	   List<string> tags = new List<string>();
	   tags.Add(search_tag);
	   string query = "SELECT * FROM projects JOIN Project_tags ON Projects.ID=project_tags.Project_ID JOIN Tags ON Project_tags.Tag_ID = Tags.ID WHERE Tags.Name LIKE @tag";
	   using (SqlConnection con = new SqlConnection(connectionString))
	   {
		   using (SqlCommand sqlcommand = new SqlCommand(query, con))
		   {
			   try
			   {
				   sqlcommand.Parameters.Add(new SqlParameter("@tag", "%" + search_tag + "%"));
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
		   AppendTags(item);
	   }
	   return results;
   }*/

	#endregion
}