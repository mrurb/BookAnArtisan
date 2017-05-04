﻿using System;
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
            string sql = "SELECT booking.ID bookingID, booking.StartTime starttime, booking.Deleted deleted, booking.EndTime endtime, materials.ID materialID, materials.Name materialsname, materials.Description description, materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address FROM Bookings booking JOIN Materials_Unique materials ON booking.MaterialID = materials.ID JOIN AspNetUsers users ON booking.UserID = users.Id WHERE id = @id";
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
                                item = new Material()
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

            string sql = "SELECT booking.ID bookingID, booking.StartTime starttime, booking.Deleted deleted, booking.EndTime endtime, materials.ID materialID, materials.Name materialsname, materials.Description description, materials.Condition condition, users.ID userID, users.Email email, users.PhoneNumber phonenumber, users.UserName username, users.FirstName firstname, users.LastName lastname, users.Address address FROM Bookings booking JOIN Materials_Unique materials ON booking.MaterialID = materials.ID JOIN AspNetUsers users ON booking.UserID = users.Id";

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
                                item = new Material()
                                {
                                    Id = (int)reader["materialID"],
                                    Name = reader["materialsname"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Condition = reader["condition"].ToString(),
                                }
                            });
                        }
                    }
                }
            }
            return materials;
        }

        public Booking Update(Booking t)
        {
            string sql = "if not exists(SELECT StartTime, EndTime FROM Booking WHERE (@starttime <= EndTime AND @endtime >= StartTime) AND @starttime < @endtime AND MaterialID = @materialID AND Deleted = 0) BEGIN UPDATE Booking SET StartTime = @starttime, EndTime = @endtime, UserID = @userID, Deleted = @deleted END";
            SqlParameter[] sqlparams =
            {
                new SqlParameter { ParameterName = "@userID", SqlValue = t.User.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@materialID", SqlValue = t.item.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@deleted", SqlValue = t.Deleted, SqlDbType = SqlDbType.Bit }
            };
            SqlConnection con = new SqlConnection(connectionstring);
            SqlCommand sqlcommand = new SqlCommand(sql, con);
            SqlTransaction myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
            sqlcommand.Transaction = myTrans;
            sqlcommand.Parameters.AddRange(sqlparams);
            try
            {
                sqlcommand.Connection.Open();
                int rowsaffected = sqlcommand.ExecuteNonQuery();
                if (rowsaffected < 1)
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
    }
}