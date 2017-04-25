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
    public class RoleDB : IDataAccess<Role>
    {
        private string connectionString;

        public RoleDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

        public Role Create(Role role)
        {
            string sql = "INSERT INTO AspNetRoles VALUES(@Id, @Name)";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Id", SqlValue = role.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Name", SqlValue = role.Name, SqlDbType = SqlDbType.NVarChar }
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    role.Id = Convert.ToString(command.ExecuteScalar());
                }
            }

            return role;
        }

        public Role Read(Role role)
        {
            string sql = "SELECT Name FROM AspNetRoles WHERE Id = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = role.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int NameCol = reader.GetOrdinal("Name");

                        if(reader.Read())
                        {
                            role.Name = reader.GetString(NameCol);
                        }
                    }
                }
            }

            return role;
        }

        public Role Update(Role role)
        {
            string sql = "UPDATE AspNetRoles SET Name = @Name WHERE Id = @Id";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Id", SqlValue = role.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Name" , SqlValue = role.Name, SqlDbType = SqlDbType.NVarChar}
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (!(0 < affectedRows))
                    {
                        throw new System.Exception("No rows affected");
                    }
                }
            }
            return role;
        }
        public Role Delete(Role role)
        {
            string sql = "DELETE FROM AspNetRoles WHERE Id = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = role.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (!(0 < affectedRows))
                    {
                        throw new System.Exception("No rows affected. Delete failed - Does the object exist beforehand in the database?");
                    }
                }
            }

            return role;
        }

        public List<Role> ReadAll()
        {
            List<Role> roles = new List<Role>();

            string sql = "SELECT * FROM AspNetRoles";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("Id");
                            int NameCol = reader.GetOrdinal("Name");

                            while (reader.Read())
                            {
                                roles.Add(new Role
                                {
                                    Id = reader.GetString(IdCol),
                                    Name = reader.GetString(NameCol)
                                });
                            }
                        }
                    }
                } 
            }
            return roles;
        }
    }
}
