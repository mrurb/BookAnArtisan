﻿using Model;
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
    public class TagDB : IDataAccess<Tag>
    {
        private string connectionString;

        public TagDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["testConnection"].ConnectionString;
        }

        public Tag Create(Tag tag)
        {
            string sql = "INSERT INTO Tags VALUES ID = @Id, Name = @Name SELECT";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Id", SqlValue = tag.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Name", SqlValue = tag.Name, SqlDbType = SqlDbType.NVarChar }
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    tag.Id = Convert.ToString(command.ExecuteScalar());
                }
            }

            return tag;
        }

        

        public Tag Read(Tag tag)
        {
            string sql = "SELECT Name FROM Tags WHERE ID = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = tag.Id, SqlDbType = SqlDbType.NVarChar };

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
                            tag.Name = reader.GetString(NameCol);
                        }
                    }
                }
            }

            return tag;
        }

        public Tag Update(Tag tag)
        {
            string sql = "UPDATE Tags SET Name = @Name WHERE ID = @Id";

            SqlParameter[] arrayOfParameters =
            {
                new SqlParameter { ParameterName = "@Id", SqlValue = tag.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Name" , SqlValue = tag.Name, SqlDbType = SqlDbType.NVarChar}
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayOfParameters);
                    command.Connection.Open();
                    int affectedRows = command.ExecuteNonQuery();
                    if (!(0 < affectedRows))
                    {
                        throw new System.Exception("No rows affected. Update failed - Does the object exist beforehand in the database?");
                    }
                }
            }
            return tag;
        }
        public Tag Delete(Tag tag)
        {
            string sql = "DELETE FROM Tags WHERE Id = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = tag.Id, SqlDbType = SqlDbType.NVarChar };

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

            return tag;
        }

        public List<Tag> ReadAll()
        {
            List<Tag> tags = new List<Tag>();

            string sql = "SELECT * FROM AspNetRoles";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("ID");
                            int NameCol = reader.GetOrdinal("Name");

                            while (reader.Read())
                            {
                                tags.Add(new Tag
                                {
                                    Id = reader.GetString(IdCol),
                                    Name = reader.GetString(NameCol)
                                });
                            }
                        }
                    }
                }
            }
            return tags;
        }
    }
}