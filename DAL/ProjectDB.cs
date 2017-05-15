using System;
using System.Collections.Generic;
using Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ProjectDB : IDataAccess<Project>
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        public Project Create(Project project)
        {
            string sql = "INSERT INTO Projects VALUES(@Name, @Created_by_ID, @Contact_ID, @Project_status_ID, @Project_description, @Street_Name, @Start_time, @Created, @Modified, @Deleted) SELECT SCOPE_IDENTITY()";

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    if (rowsaffected < 1)
                    {
                        throw new System.Exception("No rows affected. Insert failed");
                    }
                }
            }
            return project;
        }

        public Project Read(Project project)
        {
            string sql = "SELECT Projects.Name, Projects.ID, Projects.Created_by_ID, Projects.Contact_ID, Projects.Project_status_ID, Projects.Project_description, Projects.Street_Name,Projects.Start_time, Projects.Created, Projects.Modified, Projects.Deleted, CreatedBy.UserName CreatedByUserName, Contact.UserName ContactUserName FROM Projects JOIN AspNetUsers CreatedBy ON Projects.Created_by_ID = CreatedBy.Id JOIN AspNetUsers Contact ON Projects.Contact_ID = Contact.Id WHERE Projects.ID = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = project.Id, SqlDbType = SqlDbType.Int };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("ID");
                            int NameCol = reader.GetOrdinal("Name");
                            int Created_by_IDCol = reader.GetOrdinal("Created_by_ID");
                            int Contact_IDCol = reader.GetOrdinal("Contact_ID");
                            int Project_status_IDCol = reader.GetOrdinal("Project_status_ID");
                            int Project_DescriptionCol = reader.GetOrdinal("Project_description");
                            int Street_NameCol = reader.GetOrdinal("Street_Name");
                            int Start_timeCol = reader.GetOrdinal("Start_time");
                            int CreatedCol = reader.GetOrdinal("Created");
                            int ModifiedCol = reader.GetOrdinal("Modified");
                            int DeletedCol = reader.GetOrdinal("Deleted");
                            int CreatedByUserNameCol = reader.GetOrdinal("CreatedByUserName");
                            int ContactUserNameCol = reader.GetOrdinal("ContactUserName");

                            if (reader.Read())
                            {
                                project.Id = GetDataSafe(reader, IdCol, reader.GetInt32);
                                project.Name = GetDataSafe(reader, NameCol, reader.GetString);
                                project.CreatedBy = new User { Id = GetDataSafe(reader, Created_by_IDCol, reader.GetString), UserName = GetDataSafe(reader, CreatedByUserNameCol, reader.GetString) };
                                project.Contact = new User { Id = GetDataSafe(reader, Contact_IDCol, reader.GetString), UserName = GetDataSafe(reader, ContactUserNameCol, reader.GetString) };
                                project.ProjectStatusID = GetDataSafe(reader, Project_status_IDCol, reader.GetInt32);
                                project.ProjectDescription = GetDataSafe(reader, Project_DescriptionCol, reader.GetString);
                                project.StreetName = GetDataSafe(reader, Street_NameCol, reader.GetString);
                                project.StartTime = GetDataSafe(reader, Start_timeCol, reader.GetDateTime);
                                project.Created = GetDataSafe(reader, CreatedCol, reader.GetDateTime);
                                project.Modified = GetDataSafe(reader, ModifiedCol, reader.GetDateTime);
                                project.Deleted = GetDataSafe(reader, DeletedCol, reader.GetBoolean);
                            }
                        }
                    }
                }
            }
            return project;
        }

        public Project Update(Project project)
        {
            string sql = "UPDATE Projects SET Name = @Name, Created_by_ID = @Created_by_ID, Contact_ID = @Contact_ID, Project_status_ID = @Project_status_ID, Project_description = @Project_description, Street_Name = @Street_Name, Start_time = @Start_time, Created = @Created, Modified = @Modified, Deleted = @Deleted WHERE ID = @Id";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Id", SqlValue = project.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Name", SqlValue = project.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Created_by_ID", SqlValue = project.CreatedBy, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Contact_ID", SqlValue = project.Contact, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Project_status_ID", SqlValue = project.ProjectStatusID, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Project_description", SqlValue = project.ProjectDescription, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@Street_Name", SqlValue = project.StreetName, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Start_time", SqlValue = project.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Created", SqlValue = project.Created, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Modified", SqlValue = project.Modified, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Deleted", SqlValue = Convert.ToInt32(project.Deleted), SqlDbType = SqlDbType.Bit }
            };
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand sqlcommand = new SqlCommand(sql, con);
            con.Open();
            SqlTransaction myTrans = con.BeginTransaction(IsolationLevel.Serializable);
            sqlcommand.Transaction = myTrans;
            sqlcommand.Parameters.AddRange(arrayOfParameters);
                int affectedRows = sqlcommand.ExecuteNonQuery();
                if (!(0 < affectedRows))
                {
                    throw new Exception("No rows affected. Update failed.");
                }
            myTrans.Commit();
            return project;
        }

        public Project Delete(Project project)
        {
            int bitRepresentationOfBool = Convert.ToInt32(true);

            string sql = "UPDATE Projects SET Deleted = @Deleted WHERE ID = @Id";

            SqlParameter boolParameter = new SqlParameter { ParameterName = "@Deleted", SqlValue = bitRepresentationOfBool, SqlDbType = SqlDbType.Bit };
            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = project.Id, SqlDbType = SqlDbType.Int };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
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

        public List<Project> ReadAll()
        {
            List<Project> projects = new List<Project>();

            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT TOP(10) Projects.Name, Projects.ID, Projects.Created_by_ID, Projects.Contact_ID, Projects.Project_status_ID, Projects.Project_description, Projects.Street_Name,Projects.Start_time, Projects.Created, Projects.Modified, Projects.Deleted, CreatedBy.UserName CreatedByUserName, Contact.UserName ContactUserName FROM Projects JOIN AspNetUsers CreatedBy ON Projects.Created_by_ID = CreatedBy.Id JOIN AspNetUsers Contact ON Projects.Contact_ID = Contact.Id WHERE Projects.Deleted = 0; COMMIT";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("ID");
                            int NameCol = reader.GetOrdinal("Name");
                            int Created_by_IDCol = reader.GetOrdinal("Created_by_ID");
                            int Contact_IDCol = reader.GetOrdinal("Contact_ID");
                            int Project_status_IDCol = reader.GetOrdinal("Project_status_ID");
                            int Project_DescriptionCol = reader.GetOrdinal("Project_description");
                            int Street_NameCol = reader.GetOrdinal("Street_Name");
                            int Start_timeCol = reader.GetOrdinal("Start_time");
                            int CreatedCol = reader.GetOrdinal("Created");
                            int ModifiedCol = reader.GetOrdinal("Modified");
                            int DeletedCol = reader.GetOrdinal("Deleted");
                            int CreatedByUserNameCol = reader.GetOrdinal("CreatedByUserName");
                            int ContactUserNameCol = reader.GetOrdinal("ContactUserName");

                            while (reader.Read())
                            {
                                projects.Add(new Project
                                {
                                    Id = GetDataSafe(reader, IdCol, reader.GetInt32),
                                    Name = GetDataSafe(reader, NameCol, reader.GetString),
                                    CreatedBy = new User { Id = GetDataSafe(reader, Created_by_IDCol, reader.GetString), UserName = GetDataSafe(reader, CreatedByUserNameCol, reader.GetString) },
                                    Contact = new User { Id = GetDataSafe(reader, Contact_IDCol, reader.GetString), UserName = GetDataSafe(reader, ContactUserNameCol, reader.GetString) },
                                    ProjectStatusID = GetDataSafe(reader, Project_status_IDCol, reader.GetInt32),
                                    ProjectDescription = GetDataSafe(reader, Project_DescriptionCol, reader.GetString),
                                    StreetName = GetDataSafe(reader, Street_NameCol, reader.GetString),
                                    StartTime = GetDataSafe(reader, Start_timeCol, reader.GetDateTime),
                                    Created = GetDataSafe(reader, CreatedCol, reader.GetDateTime),
                                    Modified = GetDataSafe(reader, ModifiedCol, reader.GetDateTime),
                                    Deleted = GetDataSafe(reader, DeletedCol, reader.GetBoolean),
                                });
                            }
                        }
                    }
                }
            }
            return projects;
        }


        public List<Project> ReadAllForUser(User user)
        {
            List<Project> projects = new List<Project>();

            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT TOP(10) Projects.Name, Projects.ID, Projects.Created_by_ID, Projects.Contact_ID, Projects.Project_status_ID, Projects.Project_description, Projects.Street_Name,Projects.Start_time, Projects.Created, Projects.Modified, Projects.Deleted, CreatedBy.UserName CreatedByUserName, Contact.UserName ContactUserName FROM Projects JOIN AspNetUsers CreatedBy ON Projects.Created_by_ID = CreatedBy.Id JOIN AspNetUsers Contact ON Projects.Contact_ID = Contact.Id WHERE Projects.Created_by_ID = @Id OR Projects.Contact_ID = @Id; COMMIT";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("ID");
                            int NameCol = reader.GetOrdinal("Name");
                            int Created_by_IDCol = reader.GetOrdinal("Created_by_ID");
                            int Contact_IDCol = reader.GetOrdinal("Contact_ID");
                            int Project_status_IDCol = reader.GetOrdinal("Project_status_ID");
                            int Project_DescriptionCol = reader.GetOrdinal("Project_description");
                            int Street_NameCol = reader.GetOrdinal("Street_Name");
                            int Start_timeCol = reader.GetOrdinal("Start_time");
                            int CreatedCol = reader.GetOrdinal("Created");
                            int ModifiedCol = reader.GetOrdinal("Modified");
                            int DeletedCol = reader.GetOrdinal("Deleted");
                            int CreatedByUserNameCol = reader.GetOrdinal("CreatedByUserName");
                            int ContactUserNameCol = reader.GetOrdinal("ContactUserName");

                            while (reader.Read())
                            {
                                projects.Add(new Project
                                {
                                    Id = GetDataSafe(reader, IdCol, reader.GetInt32),
                                    Name = GetDataSafe(reader, NameCol, reader.GetString),
                                    CreatedBy = new User { Id = GetDataSafe(reader, Created_by_IDCol, reader.GetString), UserName = GetDataSafe(reader, CreatedByUserNameCol, reader.GetString) },
                                    Contact = new User { Id = GetDataSafe(reader, Contact_IDCol, reader.GetString), UserName = GetDataSafe(reader, ContactUserNameCol, reader.GetString) },
                                    ProjectStatusID = GetDataSafe(reader, Project_status_IDCol, reader.GetInt32),
                                    ProjectDescription = GetDataSafe(reader, Project_DescriptionCol, reader.GetString),
                                    StreetName = GetDataSafe(reader, Street_NameCol, reader.GetString),
                                    StartTime = GetDataSafe(reader, Start_timeCol, reader.GetDateTime),
                                    Created = GetDataSafe(reader, CreatedCol, reader.GetDateTime),
                                    Modified = GetDataSafe(reader, ModifiedCol, reader.GetDateTime),
                                    Deleted = GetDataSafe(reader, DeletedCol, reader.GetBoolean),
                                });
                            }
                        }
                    }
                }
            }
            return projects;
        }
        

        public T GetDataSafe<T>(SqlDataReader reader, int columnIndex, Func<int, T> getData)
        {
            if (!reader.IsDBNull(columnIndex))
            {
                return getData(columnIndex);
            }
            return default(T);
        }
    }
}
