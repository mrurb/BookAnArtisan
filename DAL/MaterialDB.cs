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
            string query = "INSERT INTO Materials(Name, Description, Condition, OwnerId) VALUES(@Name, @Description, @Condition, @OwnerId);";
            SqlParameter[] ArrayOfParams =
            {
                new SqlParameter { ParameterName = "@Name", SqlValue = t.Name, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Description", SqlValue = t.Description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@Condition", SqlValue = t.Condition, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@OwnerId", SqlValue = t.OwnderId, SqlDbType = SqlDbType.NVarChar }
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
            throw new NotImplementedException();
        }

        public Material Read(Material t)
        {
            throw new NotImplementedException();
        }

        public List<Material> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Material Update(Material t)
        {
            throw new NotImplementedException();
        }
    }
}
