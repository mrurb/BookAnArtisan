using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StatusDB : IDataAccess<Status>
    {
        private string connectionString;

        public StatusDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

        public Status Create(Status status)
        {
            //check if it exist ....????
            string sql = "INSERT INTO Project_status VALUES(@Name) SELECT SCOPE_IDENTITY()";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Name", SqlValue = status.Name, SqlDbType = SqlDbType.NVarChar }
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    status.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return status;
        }
        public Status Read(Status status)
        {
            string sql = "SELECT Name FROM Project_status WHERE ID = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = status.Id, SqlDbType = SqlDbType.Int };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int NameCol = reader.GetOrdinal("Name");

                        if (reader.Read())
                        {
                            status.Name = reader.GetString(NameCol);
                        }
                    }
                }
            }

            return status;
        }
        public Status Update(Status status)
        {
            string sql = "UPDATE Project_status SET Name = @Name WHERE ID = @Id";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Id", SqlValue = status.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Name" , SqlValue = status.Name, SqlDbType = SqlDbType.NVarChar}
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (affectedRows < 1)
                    {
                        throw new System.Exception("No rows affected");
                    }
                }
            }
            return status;
        }

        public Status Delete(Status status)
        {
            string sql = "DELETE FROM Project_status WHERE ID = @Id";
            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = status.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (affectedRows < 1)
                    {
                        return status;
                    }
                }
            }

            return status;
        }

        public List<Status> ReadAll()
        {
            List<Status> status = new List<Status>();

            string sql = "SELECT * FROM Project_status";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("Id");
                            int NameCol = reader.GetOrdinal("Name");

                            while (reader.Read())
                            {
                                status.Add(new Status
                                {
                                    Id = reader.GetInt32(IdCol),
                                    Name = reader.GetString(NameCol)
                                });
                            }
                        }
                    }
                }
            }
            return status;
        }
    }
}
