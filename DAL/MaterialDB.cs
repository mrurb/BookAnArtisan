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
            string query = "INSERT INTO Materials_Unique(Name, Description, Condition, OwnerId, Available, Deleted) VALUES(@Name, @Description, @Condition, @OwnerId, @Available, @Deleted);";
            SqlParameter[] ArrayOfParams =
            {
                new SqlParameter { ParameterName = "@Name", SqlValue = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Description", SqlValue = t.Description, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Condition", SqlValue = t.Condition, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@OwnerId", SqlValue = t.OwnerId, SqlDbType = SqlDbType.NVarChar },
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
                            throw new Exception("No rows affected. Insert failed.");
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
            string sql = "UPDATE Materials_Unique SET Deleted = 1 WHERE id = @id";
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
            string sql = "SELECT * FROM Materials_Unique WHERE id = @id";
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
                                OwnerId = reader["OwnerId"].ToString(),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Condition = reader["Condition"].ToString(),
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

            string sql = "SELECT Materials_Unique.ID, Materials_Unique.Name, Materials_Unique.Description, Materials_Unique.Condition, Materials_Unique.Deleted, Materials_Unique.Available, AspNetUsers.UserName FROM Materials_Unique JOIN AspNetUsers ON OwnerID = AspNetUsers.Id";

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
                                OwnerId = reader["UserName"].ToString(),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Condition = reader["Condition"].ToString(),
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
            string sql = "UPDATE Materials_Unique SET Name = @name, Description = @description, Condition = @condition, Deleted = @deleted, Available = @available, OwnerID = @ownerid WHERE ID = @id";
            SqlParameter[] sqlparams =
            {
                new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@name", SqlValue = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@description", SqlValue = t.Description, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@condition", SqlValue = t.Condition, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@deleted", SqlValue = t.Deleted, SqlDbType = SqlDbType.Bit },
                new SqlParameter { ParameterName = "@available", SqlValue = t.Available, SqlDbType = SqlDbType.Bit },
                new SqlParameter { ParameterName = "@ownerid", SqlValue = t.OwnerId, SqlDbType = SqlDbType.NVarChar }
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

        public IList<Material> SearchMaterials(string name)
        {
            IList<Material> list = new List<Material>();

            string sql = "SELECT Materials_Unique.ID, Materials_Unique.Name, Materials_Unique.Description, Materials_Unique.Condition, Materials_Unique.Deleted, Materials_Unique.Available, AspNetUsers.UserName FROM Materials_Unique JOIN AspNetUsers ON OwnerID = AspNetUsers.Id WHERE Name LIKE '%' + @name + '%' OR Tags LIKE '%' + @name + '%' OR Description LIKE '%' + @name + '%' OR Condition LIKE '%' + @name + '%'";

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
                                    OwnerId = reader["UserName"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Condition = reader["Condition"].ToString(),
                                    Available = (bool)reader["Available"],
                                    Deleted = (bool)reader["Deleted"]
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
