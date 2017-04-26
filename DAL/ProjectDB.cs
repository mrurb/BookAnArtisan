using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ProjectDB : IDataAccess<Project>
    {
        private string connectionString;
        public ProjectDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

        public Project Create(Project project)
        {
            string sql = "INSERT INTO Projects VALUES(@Name, @Created_by_ID, @Contact_ID, @Project_status_ID, @Project_description, @Street_Name, @Start_time, @Created, @Modified, @Deleted) SELECT SCOPE_IDENTITY()";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Name", SqlValue = project.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Created_by_ID", SqlValue = project.Created_by_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Contact_ID", SqlValue = project.Contact_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Project_status_ID", SqlValue = project.Project_status_ID, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Project_description", SqlValue = project.Project_description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@Street_Name", SqlValue = project.Street_Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Start_time", SqlValue = project.Start_time, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Created", SqlValue = project.Created, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Modified", SqlValue = project.Modified, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Deleted", SqlValue = Convert.ToInt32(project.Deleted), SqlDbType = SqlDbType.Bit }
            };
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    // Add exceptionhandling
                    project.Id = Convert.ToInt32(command.ExecuteScalar());
                }   
            }
            return project;
        }

        public Project Read(Project project)
        {
            string sql = "SELECT * FROM Projects WHERE ID = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = project.Id, SqlDbType = SqlDbType.Int };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    // Add exceptionhandling
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

                            if (reader.Read())
                            {
                                project.Id = GetDataSafe<int>(reader, IdCol, reader.GetInt32);
                                project.Name = GetDataSafe<string>(reader, NameCol, reader.GetString);
                                project.Created_by_ID = GetDataSafe<string>(reader, Created_by_IDCol, reader.GetString);
                                project.Contact_ID = GetDataSafe<string>(reader, Contact_IDCol, reader.GetString);
                                project.Project_status_ID = GetDataSafe<int>(reader, Project_status_IDCol, reader.GetInt32);
                                project.Project_description = GetDataSafe<string>(reader, Project_DescriptionCol, reader.GetString);
                                project.Street_Name = GetDataSafe<string>(reader, Street_NameCol, reader.GetString);
                                project.Start_time = GetDataSafe<DateTime>(reader, Start_timeCol, reader.GetDateTime);
                                project.Created = GetDataSafe<DateTime>(reader, CreatedCol, reader.GetDateTime);
                                project.Modified = GetDataSafe<DateTime>(reader, ModifiedCol, reader.GetDateTime);
                                project.Deleted = GetDataSafe<bool>(reader, DeletedCol, reader.GetBoolean);
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
                new SqlParameter { ParameterName = "@Created_by_ID", SqlValue = project.Created_by_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Contact_ID", SqlValue = project.Contact_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Project_status_ID", SqlValue = project.Project_status_ID, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Project_description", SqlValue = project.Project_description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@Street_Name", SqlValue = project.Street_Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Start_time", SqlValue = project.Start_time, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Created", SqlValue = project.Created, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Modified", SqlValue = project.Modified, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Deleted", SqlValue = Convert.ToInt32(project.Deleted), SqlDbType = SqlDbType.Bit }
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    // Add exceptionhandling 
                    int affectedRows = command.ExecuteNonQuery();
                    if (!(0 < affectedRows))
                    {
                        throw new System.Exception("No rows affected. Update failed - Does the object exist beforehand in the database?");
                    }
                }
            }
            return project;
        }

        //Actually deletes the object because there's currently no 'Deleted' bit in DB.
        public bool Delete(Project project)
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
                        return false;
                    }
                }
            }

            return true;
        }

        public List<Project> ReadAll()
        {
            List<Project> projects = new List<Project>();
            
            string sql = "SELECT * FROM Projects";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();
                    // Add exceptionhandling
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

                            while (reader.Read())
                            {
                                projects.Add(new Project
                                {
                                    Id = GetDataSafe<int>(reader, IdCol, reader.GetInt32),
                                    Name = GetDataSafe<string>(reader, NameCol, reader.GetString),
                                    Created_by_ID = GetDataSafe<string>(reader, Created_by_IDCol, reader.GetString),
                                    Contact_ID = GetDataSafe<string>(reader, Contact_IDCol, reader.GetString),
                                    Project_status_ID = GetDataSafe<int>(reader, Project_status_IDCol, reader.GetInt32),
                                    Project_description = GetDataSafe<string>(reader, Project_DescriptionCol, reader.GetString),
                                    Street_Name = GetDataSafe<string>(reader, Street_NameCol, reader.GetString),
                                    Start_time = GetDataSafe<DateTime>(reader, Start_timeCol, reader.GetDateTime),
                                    Created = GetDataSafe<DateTime>(reader, CreatedCol, reader.GetDateTime),
                                    Modified = GetDataSafe<DateTime>(reader, ModifiedCol, reader.GetDateTime),
                                    Deleted = GetDataSafe<bool>(reader, DeletedCol, reader.GetBoolean),
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
