using System;
using System.Collections.Generic;
using Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using static System.Int32;

namespace DAL
{
	public class ProjectDb : IDataAccess<Project>
	{
		private readonly string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

		public Project Create(Project project)
		{
			const string sql = "INSERT INTO Projects VALUES(@Name, @Created_by_ID, @Contact_ID, @Project_status_ID, @Project_description, @Street_Name, @Start_time, @Created, @Modified, @Deleted) SELECT SCOPE_IDENTITY()";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter { ParameterName = "@Name", SqlValue = project.Name, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Created_by_ID", SqlValue = project.CreatedBy.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Contact_ID", SqlValue = project.Contact.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Project_status_ID", SqlValue = project.ProjectStatusID, SqlDbType = SqlDbType.Int },
				new SqlParameter { ParameterName = "@Project_description", SqlValue = project.ProjectDescription, SqlDbType = SqlDbType.Text },
				new SqlParameter { ParameterName = "@Street_Name", SqlValue = project.StreetName, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Start_time", SqlValue = project.StartTime, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@Created", SqlValue = DateTime.Now, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@Modified", SqlValue = DateTime.Now, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@Deleted", SqlValue = Convert.ToInt32(project.Deleted), SqlDbType = SqlDbType.Bit }
			};

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
						
					command.Parameters.AddRange(arrayOfParameters);
					command.Connection.Open();
					// For some reason our ExecuteScalar could not be casted to int, so we made a dirty quick fix.
					project.Id = Parse(command.ExecuteScalar().ToString());
					if (project.Id == 0)
					{
						throw new Exception("No rows affected. Insert failed");
					}
				}
			}
			return project;
		}

		public Project Read(Project project)
		{
			const string sql = "SELECT Projects.Name, Projects.ID, Projects.Created_by_ID, Projects.Contact_ID, Projects.Project_status_ID, Projects.Project_description, Projects.Street_Name,Projects.Start_time, Projects.Created, Projects.Modified, Projects.Deleted, CreatedBy.UserName CreatedByUserName, Contact.UserName ContactUserName FROM Projects JOIN AspNetUsers CreatedBy ON Projects.Created_by_ID = CreatedBy.Id JOIN AspNetUsers Contact ON Projects.Contact_ID = Contact.Id WHERE Projects.ID = @Id";

			var idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = project.Id, SqlDbType = SqlDbType.Int };

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					using (var reader = command.ExecuteReader())
					{
						int projectStatusIdCol = reader.GetOrdinal("Project_status_ID");
						if (!reader.HasRows) return project;
						int idCol = reader.GetOrdinal("ID");
						int nameCol = reader.GetOrdinal("Name");
						int createdByIdCol = reader.GetOrdinal("Created_by_ID");
						int contactIdCol = reader.GetOrdinal("Contact_ID");
						int projectDescriptionCol = reader.GetOrdinal("Project_description");
						int streetNameCol = reader.GetOrdinal("Street_Name");
						int startTimeCol = reader.GetOrdinal("Start_time");
						int createdCol = reader.GetOrdinal("Created");
						int modifiedCol = reader.GetOrdinal("Modified");
						int deletedCol = reader.GetOrdinal("Deleted");
						int createdByUserNameCol = reader.GetOrdinal("CreatedByUserName");
						int contactUserNameCol = reader.GetOrdinal("ContactUserName");

						if (!reader.Read()) return project;
						project.Id = GetDataSafe(reader, idCol, reader.GetInt32);
						project.Name = GetDataSafe(reader, nameCol, reader.GetString);
						project.CreatedBy = new User { Id = GetDataSafe(reader, createdByIdCol, reader.GetString), UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString) };
						project.Contact = new User { Id = GetDataSafe(reader, contactIdCol, reader.GetString), UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString) };
						project.ProjectStatusID = GetDataSafe(reader, projectStatusIdCol, reader.GetInt32);
						project.ProjectDescription = GetDataSafe(reader, projectDescriptionCol, reader.GetString);
						project.StreetName = GetDataSafe(reader, streetNameCol, reader.GetString);
						project.StartTime = GetDataSafe(reader, startTimeCol, reader.GetDateTime);
						project.Created = GetDataSafe(reader, createdCol, reader.GetDateTime);
						project.Modified = GetDataSafe(reader, modifiedCol, reader.GetDateTime);
						project.Deleted = GetDataSafe(reader, deletedCol, reader.GetBoolean);
					}
				}
			}
			return project;
		}

		public Project Update(Project project)
		{
			const string sql = "UPDATE Projects SET Name = @Name, Created_by_ID = @Created_by_ID, Contact_ID = @Contact_ID, Project_status_ID = @Project_status_ID, Project_description = @Project_description, Street_Name = @Street_Name, Start_time = @Start_time, Created = @Created, Modified = @Modified, Deleted = @Deleted WHERE ID = @Id";

			SqlParameter[] arrayOfParameters =
			{
				new SqlParameter { ParameterName = "@Id", SqlValue = project.Id, SqlDbType = SqlDbType.Int },
				new SqlParameter { ParameterName = "@Name", SqlValue = project.Name, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Created_by_ID", SqlValue = project.CreatedBy.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Contact_ID", SqlValue = project.Contact.Id, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Project_status_ID", SqlValue = project.ProjectStatusID, SqlDbType = SqlDbType.Int },
				new SqlParameter { ParameterName = "@Project_description", SqlValue = project.ProjectDescription, SqlDbType = SqlDbType.Text },
				new SqlParameter { ParameterName = "@Street_Name", SqlValue = project.StreetName, SqlDbType = SqlDbType.NVarChar },
				new SqlParameter { ParameterName = "@Start_time", SqlValue = project.StartTime, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@Created", SqlValue = project.Created, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@Modified", SqlValue = project.Modified, SqlDbType = SqlDbType.DateTime },
				new SqlParameter { ParameterName = "@Deleted", SqlValue = Convert.ToInt32(project.Deleted), SqlDbType = SqlDbType.Bit }
			};
			var con = new SqlConnection(connectionString);
			var sqlcommand = new SqlCommand(sql, con);
			con.Open();
			var myTrans = con.BeginTransaction(IsolationLevel.RepeatableRead);
			sqlcommand.Transaction = myTrans;
			sqlcommand.Parameters.AddRange(arrayOfParameters);
			int affectedRows = sqlcommand.ExecuteNonQuery();
			if (affectedRows < 1)
			{
				throw new Exception("No rows affected. Update failed.");
			}
			myTrans.Commit();
			return project;
		}

		public Project Delete(Project project)
		{
			int bitRepresentationOfBool = Convert.ToInt32(true);

			const string sql = "UPDATE Projects SET Deleted = @Deleted WHERE ID = @Id";

			var boolParameter = new SqlParameter { ParameterName = "@Deleted", SqlValue = bitRepresentationOfBool, SqlDbType = SqlDbType.Bit };
			var idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = project.Id, SqlDbType = SqlDbType.Int };

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(boolParameter);
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					int affectedrows = command.ExecuteNonQuery();
					if (affectedrows < 1)
					{
						return project;
					}
				}
			}

			return project;
		}

		public Project RemoveProject(Project project)
		{
			const string sql = "DELETE FROM Projects WHERE id = @id";
			var theparam = new SqlParameter { ParameterName = "@id", SqlValue = project.Id, SqlDbType = SqlDbType.Int };
			using (var connection = new SqlConnection(connectionString)) 
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(theparam);
					command.Connection.Open();
					using (command.ExecuteReader())
					{
					}
				}
			}
			return project;
		}

		public List<Project> ReadAll()
		{
			var projects = new List<Project>();

			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT TOP(10) Projects.Name, Projects.ID, Projects.Created_by_ID, Projects.Contact_ID, Projects.Project_status_ID, Projects.Project_description, Projects.Street_Name,Projects.Start_time, Projects.Created, Projects.Modified, Projects.Deleted, CreatedBy.UserName CreatedByUserName, Contact.UserName ContactUserName FROM Projects JOIN AspNetUsers CreatedBy ON Projects.Created_by_ID = CreatedBy.Id JOIN AspNetUsers Contact ON Projects.Contact_ID = Contact.Id WHERE Projects.Deleted = 0; COMMIT";

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return projects;
						int idCol = reader.GetOrdinal("ID");
						int nameCol = reader.GetOrdinal("Name");
						int createdByIdCol = reader.GetOrdinal("Created_by_ID");
						int contactIdCol = reader.GetOrdinal("Contact_ID");
						int projectStatusIdCol = reader.GetOrdinal("Project_status_ID");
						int projectDescriptionCol = reader.GetOrdinal("Project_description");
						int streetNameCol = reader.GetOrdinal("Street_Name");
						int startTimeCol = reader.GetOrdinal("Start_time");
						int createdCol = reader.GetOrdinal("Created");
						int modifiedCol = reader.GetOrdinal("Modified");
						int deletedCol = reader.GetOrdinal("Deleted");
						int createdByUserNameCol = reader.GetOrdinal("CreatedByUserName");
						int contactUserNameCol = reader.GetOrdinal("ContactUserName");

						while (reader.Read())
						{
							projects.Add(new Project
							{
								Id = GetDataSafe(reader, idCol, reader.GetInt32),
								Name = GetDataSafe(reader, nameCol, reader.GetString),
								CreatedBy = new User { Id = GetDataSafe(reader, createdByIdCol, reader.GetString), UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString) },
								Contact = new User { Id = GetDataSafe(reader, contactIdCol, reader.GetString), UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString) },
								ProjectStatusID = GetDataSafe(reader, projectStatusIdCol, reader.GetInt32),
								ProjectDescription = GetDataSafe(reader, projectDescriptionCol, reader.GetString),
								StreetName = GetDataSafe(reader, streetNameCol, reader.GetString),
								StartTime = GetDataSafe(reader, startTimeCol, reader.GetDateTime),
								Created = GetDataSafe(reader, createdCol, reader.GetDateTime),
								Modified = GetDataSafe(reader, modifiedCol, reader.GetDateTime),
								Deleted = GetDataSafe(reader, deletedCol, reader.GetBoolean),
							});
						}
					}
				}
			}
			return projects;
		}


		public List<Project> ReadAllForUser(User user)
		{
			var projects = new List<Project>();

			const string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT TOP(10) Projects.Name, Projects.ID, Projects.Created_by_ID, Projects.Contact_ID, Projects.Project_status_ID, Projects.Project_description, Projects.Street_Name,Projects.Start_time, Projects.Created, Projects.Modified, Projects.Deleted, CreatedBy.UserName CreatedByUserName, Contact.UserName ContactUserName FROM Projects JOIN AspNetUsers CreatedBy ON Projects.Created_by_ID = CreatedBy.Id JOIN AspNetUsers Contact ON Projects.Contact_ID = Contact.Id WHERE Projects.Created_by_ID = @Id OR Projects.Contact_ID = @Id; COMMIT";

			var idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar };

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(idParameter);
					command.Connection.Open();
					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows) return projects;
						int idCol = reader.GetOrdinal("ID");
						int nameCol = reader.GetOrdinal("Name");
						int createdByIdCol = reader.GetOrdinal("Created_by_ID");
						int contactIdCol = reader.GetOrdinal("Contact_ID");
						int projectStatusIdCol = reader.GetOrdinal("Project_status_ID");
						int projectDescriptionCol = reader.GetOrdinal("Project_description");
						int streetNameCol = reader.GetOrdinal("Street_Name");
						int startTimeCol = reader.GetOrdinal("Start_time");
						int createdCol = reader.GetOrdinal("Created");
						int modifiedCol = reader.GetOrdinal("Modified");
						int deletedCol = reader.GetOrdinal("Deleted");
						int createdByUserNameCol = reader.GetOrdinal("CreatedByUserName");
						int contactUserNameCol = reader.GetOrdinal("ContactUserName");

						while (reader.Read())
						{
							projects.Add(new Project
							{
								Id = GetDataSafe(reader, idCol, reader.GetInt32),
								Name = GetDataSafe(reader, nameCol, reader.GetString),
								CreatedBy = new User { Id = GetDataSafe(reader, createdByIdCol, reader.GetString), UserName = GetDataSafe(reader, createdByUserNameCol, reader.GetString) },
								Contact = new User { Id = GetDataSafe(reader, contactIdCol, reader.GetString), UserName = GetDataSafe(reader, contactUserNameCol, reader.GetString) },
								ProjectStatusID = GetDataSafe(reader, projectStatusIdCol, reader.GetInt32),
								ProjectDescription = GetDataSafe(reader, projectDescriptionCol, reader.GetString),
								StreetName = GetDataSafe(reader, streetNameCol, reader.GetString),
								StartTime = GetDataSafe(reader, startTimeCol, reader.GetDateTime),
								Created = GetDataSafe(reader, createdCol, reader.GetDateTime),
								Modified = GetDataSafe(reader, modifiedCol, reader.GetDateTime),
								Deleted = GetDataSafe(reader, deletedCol, reader.GetBoolean),
							});
						}
					}
				}
			}
			return projects;
		}


		public T GetDataSafe<T>(SqlDataReader reader, int columnIndex, Func<int, T> getData)
		{
			return !reader.IsDBNull(columnIndex) ? getData(columnIndex) : default(T);
		}
	}
}
