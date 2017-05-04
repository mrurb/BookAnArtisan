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
    public class RentingDB : IDataAccess<Booking>
    {
        static string connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        public Booking Create(Booking t)
        {
            SqlParameter[] ArrayOfParams =
            {
                new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@userID", SqlValue = t.User.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter {ParameterName = "@materialID" , SqlValue = t.item.Id, SqlDbType = SqlDbType.NVarChar }
           };
            SqlConnection con = new SqlConnection(connectionstring);
            string query = "if not exists(SELECT StartTime, EndTime FROM Booking WHERE (@starttime <= EndTime AND @endtime >= StartTime) AND @starttime < @endtime AND MaterialID = @materialID AND Deleted = 0) BEGIN INSERT INTO Booking(StartTime, EndTime, UserID, MaterialID) VALUES(@starttime, @endtime, @userID, @materialID) END";
            SqlCommand sqlcommand = new SqlCommand(query, con);
            SqlTransaction myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlcommand.Transaction = myTrans;
            sqlcommand.Parameters.AddRange(ArrayOfParams);
            try
            {
                con.Open();
                int rowsaffected = sqlcommand.ExecuteNonQuery();
                if(rowsaffected < 1)
                {
                    con.Close();
                    throw new Exception();
                }
                myTrans.Commit();
            }
                catch (Exception)
                {
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch (SqlException)
                    {
                        //rollback failed
                        throw new Exception();
                    }
                finally
                {
                    con.Close();
                }
                    //transaction failed
                }
                finally
                {
                    con.Close();
                }
            return t;
        }

        public Booking Delete(Booking t)
        {
            string sql = "UPDATE Rented SET Deleted = 1 WHERE id = @id";
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

        public Booking Read(Booking t)
        {
            string sql = "SELECT * FROM Rented WHERE id = @id";
            Booking material = null;
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
                            material = new Booking
                            {
                                Id = (int)reader["Id"],
                                EndTime = (DateTime)reader["EndTime"],
                                StartTime = (DateTime)reader["StartTime"],
                                Deleted = (bool)reader["Deleted"]
                            };
                        }
                    }
                }
            }
            return material;
        }

        public List<Booking> ReadAll()
        {
            List<Booking> materials = new List<Booking>();

            string sql = "SELECT * FROM Rented";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materials.Add(new Booking
                            {
                                Id = (int)reader["Id"],
                                EndTime = (DateTime)reader["EndTime"],
                                StartTime = (DateTime)reader["StartTime"],
                                Deleted = (bool)reader["Deleted"]
                            });
                        }
                    }
                }
            }
            return materials;
        }

        public Booking Update(Booking t)
        {
            string sql = "UPDATE Rented SET StartTime = @starttime, EndTime = @endtime, Deleted = @deleted WHERE ID = @id";
            SqlParameter[] sqlparams =
            {
                new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@deleted", SqlValue = t.Deleted, SqlDbType = SqlDbType.Bit }
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
    }
}
