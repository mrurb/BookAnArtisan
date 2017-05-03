using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace DAL
{
    public class MaterialDB : IDataAccess<Material>
    {
        static string connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        public Material Create(Material t)
        {
            string query = "INSERT INTO Materials(Name, Description, Condition, OwnerId, Amount, Available, Deleted) VALUES(@Name, @Description, @Condition, @OwnerId, @Amount, @Available, @Deleted);";
            SqlParameter[] ArrayOfParams =
            {
                new SqlParameter { ParameterName = "@Name", SqlValue = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Description", SqlValue = t.Description, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Condition", SqlValue = t.Condition, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@OwnerId", SqlValue = t.OwnderId, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Amount", SqlValue = t.Amount, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@Available", SqlValue = t.Available, SqlDbType = SqlDbType.Bit },
                new SqlParameter { ParameterName = "@Deleted", SqlValue = t.Deleted, SqlDbType = SqlDbType.Bit }
           };

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcommand = new SqlCommand(query, con))
                {
                    try
                    {
                        sqlcommand.Parameters.AddRange(ArrayOfParams);
                        con.Open();
                        int rowsaffected = sqlcommand.ExecuteNonQuery();
                        if (rowsaffected < 1)
                        {
                            throw new System.Exception("No rows affected. Insert failed - Are you a fag?");
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                }
            }
            return t;
        }

        public Material Delete(Material t)
        {
            string sql = "UPDATE Materials SET Deleted = 1 WHERE id = @id";
            SqlParameter theparam = new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int };
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(theparam);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    }
                }
            }
            return null;
        }

        public Material Read(Material t)
        {
            string sql = "SELECT * FROM Materials WHERE id = @id";
            Material material = null;
            SqlParameter theparam = new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int };
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(theparam);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            material = new Material
                            {
                                Id = (int)reader["Id"],
                                Description = reader["Description"].ToString(),
                                Condition = reader["Condition"].ToString(),
                                Name = reader["Name"].ToString(),
                                OwnderId = reader["OwnerId"].ToString(),
                                Amount = (int)reader["Amount"],
                                Available = (bool)reader["Available"],
                                Deleted = (bool)reader["Deleted"]
                            };
                        }
                    }
                }
            }
            return material;
        }

        public List<Material> ReadAll()
        {
            List<Material> materials = new List<Material>();

            string sql = "SELECT * FROM Materials";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materials.Add(new Material
                            {
                                Id = (int)reader["Id"],
                                Description = reader["Description"].ToString(),
                                Condition = reader["Condition"].ToString(),
                                Name = reader["Name"].ToString(),
                                OwnderId = reader["OwnerId"].ToString(),
                                Amount = (int)reader["Amount"],
                                Available = (bool)reader["Available"],
                                Deleted = (bool)reader["Deleted"]
                            });
                        }
                    }
                }
            }
            return materials;
        }

        public Material Update(Material t)
        {
            string sql = "UPDATE Materials SET Name = @name, Description = @description, Condition = @condition, Deleted = @deleted, Available = @available, OwnerID = @ownerid, Amount = @amount WHERE ID = @id";
            SqlParameter[] sqlparams =
            {
                new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@name", SqlValue = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@description", SqlValue = t.Description, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@condition", SqlValue = t.Condition, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@deleted", SqlValue = t.Deleted, SqlDbType = SqlDbType.Bit },
                new SqlParameter { ParameterName = "@available", SqlValue = t.Available, SqlDbType = SqlDbType.Bit },
                new SqlParameter { ParameterName = "@ownerid", SqlValue = t.OwnderId, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@amount", SqlValue = t.Amount, SqlDbType = SqlDbType.Int }
            };
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(sqlparams);
                    command.Connection.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    if (rowsaffected < 1)
                    {
                        throw new Exception();
                    }
                }
            }
            return t;
        }

        public IList<Material> SearchByMaterialName(string name)
        {
            IList<Material> list = new List<Material>();

            string sql = "SELECT * FROM Materials WHERE Materials.Name LIKE '%' + @name + '%' OR Materials.Tags LIKE '%' + @name + '%' OR Materials.Description LIKE '%' + @name + '%' OR Materials.Condition LIKE '%' + @name + '%'";

            SqlParameter searchParams = new SqlParameter { ParameterName = "@name", SqlValue = name, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.Add(searchParams);
                    command.Connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(
                                new Material
                                {
                                    Id = (int)reader["ID"],
                                    Description = (string)reader["Description"],
                                    Amount = (int)reader["Amount"],
                                    Available = (bool)reader["Available"],
                                    Condition = (string)reader["Condition"],
                                    Deleted = (bool)reader["Deleted"],
                                    Name = (string)reader["Name"],
                                    OwnderId = (string)reader["OwnerId"]

                                }
                            );
                        }
                    }
                }
            }
            return list;
        }
    }
}
