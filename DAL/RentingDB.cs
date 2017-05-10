using System;
using System.Collections.Generic;
using Model;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class RentingDb : IDataAccess<Booking>
    {
        static readonly string Connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        public Booking Create(Booking t)
        {
            SqlParameter[] arrayOfParams =
            {
                new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@userID", SqlValue = t.User.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@materialID" , SqlValue = t.Item.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Updated" , SqlValue = t.Updated, SqlDbType = SqlDbType.DateTime }
           };
            SqlConnection con = new SqlConnection(Connectionstring);
            string query = "if not exists(SELECT StartTime, EndTime FROM Bookings WHERE (@starttime <= EndTime AND @endtime >= StartTime) AND @starttime < @endtime AND MaterialID = @materialID AND Bookings.Deleted = 0) BEGIN INSERT INTO Bookings(StartTime, EndTime, UserID, MaterialID) VALUES(@starttime, @endtime, @userID, @materialID) END";
            SqlCommand sqlcommand = new SqlCommand(query, con);


            try
            {
                con.Open();
                SqlTransaction myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
                sqlcommand.Transaction = myTrans;
                sqlcommand.Parameters.AddRange(arrayOfParams);
                var rowsaffected = sqlcommand.ExecuteNonQuery();
                if (rowsaffected < 1)
                {
                    con.Close();
                    throw new Exception();
                }
                myTrans.Commit();
            }
            catch (Exception)
            {
                con.Close();
                throw new Exception();
            }
            finally
            {
                con.Close();
            }
            return t;
        }

        public Booking Delete(Booking t)
        {
            string sql = "UPDATE Bookings SET Deleted = 1 WHERE id = @id";
            SqlParameter theparam = new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int };
            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(theparam);
                    command.Connection.Open();
                    using (command.ExecuteReader())
                    {
                    }
                }
            }
            return null;
        }

        public Booking Read(Booking t)
        {
            string sql = "SELECT Bookings.ID bookingID, Bookings.StartTime starttime, Bookings.Deleted deleted, Bookings.EndTime endtime, Bookings.Updated, Bookings.Created, materials.ID materialID, materials.Name materialsname, materials.Description description, materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address FROM Bookings JOIN Materials_Unique materials ON Bookings.MaterialID = materials.ID JOIN AspNetUsers users ON Bookings.UserID = users.Id WHERE bookings.ID = @id";
            Booking material = null;
            SqlParameter theparam = new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int };
            using (SqlConnection connection = new SqlConnection(Connectionstring))
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
                                Id = (int)reader["bookingID"],
                                EndTime = (DateTime)reader["endtime"],
                                StartTime = (DateTime)reader["starttime"],
                                Deleted = (bool)reader["deleted"],
                                Created = (DateTime)reader["Created"],
                                Updated = (DateTime)reader["Updated"],
                                User = new User()
                                {
                                    Id = reader["userID"].ToString(),
                                    FirstName = reader["firstname"].ToString(),
                                    LastName = reader["lastname"].ToString(),
                                    Email = reader["email"].ToString(),
                                    PhoneNumber = reader["phonenumber"].ToString(),
                                    Address = reader["address"].ToString(),
                                    UserName = reader["username"].ToString()
                                },
                                Item = new Material()
                                {
                                    Id = (int)reader["materialID"],
                                    Name = reader["materialsname"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Condition = reader["condition"].ToString(),
                                }
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

            string sql = "SELECT booking.ID bookingID, booking.StartTime starttime, booking.Deleted deleted, booking.EndTime endtime, materials.ID materialID, materials.Name materialsname, materials.Description description, materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address FROM Bookings booking JOIN Materials_Unique materials ON booking.MaterialID = materials.ID JOIN AspNetUsers users ON booking.UserID = users.Id WHERE booking.deleted = 0";
            SqlConnection con = new SqlConnection(Connectionstring);
            SqlCommand command = new SqlCommand(sql, con);
            try
            {
                con.Open();
                SqlTransaction myTrans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
                command.Transaction = myTrans;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        materials.Add(new Booking
                        {
                            Id = (int)reader["bookingID"],
                            EndTime = (DateTime)reader["endtime"],
                            StartTime = (DateTime)reader["starttime"],
                            Deleted = (bool)reader["deleted"],
                            User = new User()
                            {
                                Id = reader["userID"].ToString(),
                                FirstName = reader["firstname"].ToString(),
                                LastName = reader["lastname"].ToString(),
                                Email = reader["email"].ToString(),
                                PhoneNumber = reader["phonenumber"].ToString(),
                                Address = reader["address"].ToString(),
                                UserName = reader["username"].ToString()
                            },
                            Item = new Material()
                            {
                                Id = (int)reader["materialID"],
                                Name = reader["materialsname"].ToString(),
                                Description = reader["description"].ToString(),
                                Condition = reader["condition"].ToString(),
                            }
                        });
                    }
                }
                myTrans.Commit();
            }
            catch (Exception)
            {
                con.Close();
                throw new Exception();
            }
            finally
            {
                con.Close();
            }
            return materials;
        }

        public Booking Update(Booking t)
        {
            var sql = "if not exists(SELECT StartTime, EndTime FROM Bookings WHERE (@starttime <= EndTime AND @endtime >= StartTime) AND @starttime < @endtime AND MaterialID = @materialID AND Deleted = 0 AND Bookings.ID <> @id) BEGIN UPDATE Bookings SET StartTime = @starttime, EndTime = @endtime, UserID = @userID, Updated = GETUTCDATE() WHERE Bookings.ID = @id AND Bookings.Updated = @Updated END";
            
            SqlParameter[] sqlparams =
            {
                new SqlParameter { ParameterName = "@userID", SqlValue = t.User.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@materialID", SqlValue = t.Item.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@updated", SqlValue = t.Updated, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int }
            };
            SqlConnection con = new SqlConnection(Connectionstring);
            SqlCommand sqlcommand = new SqlCommand(sql, con);
            con.Open();
            SqlTransaction myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                
                sqlcommand.Transaction = myTrans;
                sqlcommand.Parameters.AddRange(sqlparams);
                
                int rowsaffected = sqlcommand.ExecuteNonQuery();
                if (rowsaffected < 1)
                {
                    throw new Exception("Concurrency problem");
                }
                myTrans.Commit();
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                throw new Exception("failed", ex);

            }
            finally
            {
                con.Close();
            }
            return t;
        }
    }
}
