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
        public Project Create(Project t)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;

            string sql = "INSERT INTO Projects VALUES(@Name, @Created_by_ID, @Contact_ID, @Project_status_ID, @Project_description, @Street_Name, @Start_time, @Created, @Modified, @Deleted) SELECT SCOPE_IDENTITY()";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Name", Value = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Created_by_ID", Value = t.Created_by_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Contact_ID", Value = t.Contact_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Project_status_ID", Value = t.Project_status_ID, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Project_description", Value = t.Project_description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@Street_Name", Value = t.Street_Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Start_time", Value = t.Start_time, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Created", Value = t.Created, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Modified", Value = t.Modified, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Deleted", Value = Convert.ToInt32(t.Deleted), SqlDbType = SqlDbType.Bit }
            };
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    // Add exceptionhandling
                    t.ID = Convert.ToInt32(command.ExecuteScalar());
                    
                }   
            }
            return t;
        }

        public Project Read(int id)
        {
            Project project = new Project();

            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;

            string sql = "SELECT * FROM Projects WHERE ID = @id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@id", SqlValue = id, SqlDbType = SqlDbType.Int };

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
                            if (reader.Read())
                            {
                                project.ID = reader.GetInt32(reader.GetOrdinal("Id"));
                                project.Name = reader.GetString(reader.GetOrdinal("Name"));
                                project.Created_by_ID = reader.GetString(reader.GetOrdinal("Created_by_ID"));
                                project.Contact_ID = reader.GetString(reader.GetOrdinal("Contact_ID"));
                                project.Project_status_ID = reader.GetInt32(reader.GetOrdinal("Project_status_ID"));
                                project.Project_description = reader.GetString(reader.GetOrdinal("Project_description"));
                                project.Street_Name = reader.GetString(reader.GetOrdinal("Street_Name"));
                                project.Start_time = reader.GetDateTime(reader.GetOrdinal("Start_time"));
                                project.Created = reader.GetDateTime(reader.GetOrdinal("Created"));
                                project.Modified = reader.GetDateTime(reader.GetOrdinal("Modified"));
                                project.Deleted = reader.GetBoolean(reader.GetOrdinal("Deleted"));
                            }
                        }
                    }    
                }
            }
            return project;
        }

        public Project Update(Project t)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;

            string sql = "UPDATE Projects SET Name = @Name, Created_by_ID = @Created_by_ID, Contact_ID = @Contact_ID, Project_status_ID = @Project_status_ID, Project_description = @Project_description, Street_Name = @Street_Name, Start_time = @Start_time, Created = @Created, Modified = @Modified, Deleted = @Deleted WHERE id = @id";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@id", SqlValue = t.ID, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Name", SqlValue = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Created_by_ID", SqlValue = t.Created_by_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Contact_ID", SqlValue = t.Contact_ID, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Project_status_ID", SqlValue = t.Project_status_ID, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Project_description", SqlValue = t.Project_description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@Street_Name", SqlValue = t.Street_Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Start_time", SqlValue = t.Start_time, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Created", SqlValue = t.Created, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Modified", SqlValue = t.Modified, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Deleted", SqlValue = Convert.ToInt32(t.Deleted), SqlDbType = SqlDbType.Bit }
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    // Add exceptionhandling
                    command.ExecuteNonQuery();
                }
            }
            return t;
        }

        public Project Delete(Project t)
        {
            // We should work out a more centralized way to obtain a connection to the database.
            string connectionString = "Data Source=aur.dk; Database=apacta; User Id=datamatikerne; Password=A6stvHkW;";
            //string connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;
            // For reasons
            int bitRepresentationOfBool = Convert.ToInt32(true);

            string sql = "UPDATE Projects SET Deleted = @Deleted WHERE id = @id";

            SqlParameter boolParameter = new SqlParameter { ParameterName = "@Deleted", SqlValue = bitRepresentationOfBool, SqlDbType = SqlDbType.Bit };
            SqlParameter idParameter = new SqlParameter { ParameterName = "@id", SqlValue = t.ID, SqlDbType = SqlDbType.Int };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(boolParameter);
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    t.Deleted = 0 < command.ExecuteNonQuery();
                }
            }

            return t;
        }
    }
}
