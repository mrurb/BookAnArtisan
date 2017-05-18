using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DAL
{
    public class MeetingDB : IDataAccess<Meeting>
    {
        static string connectionstring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        /*
         * Created (26/04) 
         * Need to consider concurrency
         * Exception Handling?
         * Consider Adding contacts etc by ID? sth like dat
         * RowsAffected btw sucks? shouldnt throw exception
         */
        public Meeting Create(Meeting t)
        {
            string query = "INSERT INTO Meetings(StartTime, EndTime, Title, Description, ContactID, CreatedByID) VALUES(@StartTime, @EndTime, @Title, @Description, @ContactID, @CreatedByID);";
            SqlParameter[] ArrayOfParams =
            {
                new SqlParameter { ParameterName = "@StartTime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@EndTime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Title", SqlValue = t.Title, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Description", SqlValue = t.Description, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@ContactID", SqlValue = t.Contact.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@CreatedByID", SqlValue = t.CreatedBy.Id, SqlDbType = SqlDbType.NVarChar },
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
                            throw new System.Exception("No rows affected. Insert failed");
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

        public List<Meeting> ReadAllForUser(User user)
        {
            List<Meeting> userMeetings = new List<Meeting>();

            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT DISTINCT Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID  FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Meetings.CreatedByID = @Id OR Meetings.ContactID = @Id; COMMIT";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = user.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int TitleCol = reader.GetOrdinal("Title");
                            int StartTimeCol = reader.GetOrdinal("StartTime");
                            int EndTimeCol = reader.GetOrdinal("EndTime");
                            int DescCol = reader.GetOrdinal("Description");
                            int CreatedByCol = reader.GetOrdinal("CreatedByID");
                            int ContactCol = reader.GetOrdinal("ContactID");
                            int IdCol = reader.GetOrdinal("mId");
                            int ContactUserNameCol = reader.GetOrdinal("Contact");
                            int CreatedByUserNameCol = reader.GetOrdinal("CreatedBy");

                            while (reader.Read())
                            {
                                userMeetings.Add(new Meeting
                                {
                                    Title = GetDataSafe(reader, TitleCol, reader.GetString),
                                    StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime),
                                    EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime),
                                    Description = GetDataSafe(reader, DescCol, reader.GetString),
                                    CreatedBy = new User { Id = GetDataSafe(reader, CreatedByCol, reader.GetString), UserName = GetDataSafe(reader, CreatedByUserNameCol, reader.GetString) },
                                    Contact = new User { Id = GetDataSafe(reader, ContactCol, reader.GetString), UserName = GetDataSafe(reader, ContactUserNameCol, reader.GetString) },
                                    Id = GetDataSafe(reader, IdCol, reader.GetInt32)
                                });
                            }
                        }
                    }
                }
            }
            return userMeetings;
        }

        public Meeting Delete(Meeting t)
        {
            string sql = "UPDATE Meetings SET Deleted = 1 WHERE ID = @Id";

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = t.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
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

        public Meeting Read(Meeting t)
        {
            string sql = "SELECT User1 CreatedByUserName, User2 ContactUserName, Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Meetings.ID = @Id"; // search by ID, see below.

            SqlParameter idParameter = new SqlParameter { ParameterName = "@Id", SqlValue = t.Id, SqlDbType = SqlDbType.NVarChar };

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(idParameter);
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int IdCol = reader.GetOrdinal("MId"); // probably don't want ID at all... but how to search then boss?
                            int TitleCol = reader.GetOrdinal("Title");
                            int StartTimeCol = reader.GetOrdinal("StartTime");
                            int EndTimeCol = reader.GetOrdinal("EndTime");
                            int DescCol = reader.GetOrdinal("Description");
                            int CreatedByIDCol = reader.GetOrdinal("CreatedByID");
                            int CreatedByNameCol = reader.GetOrdinal("CreatedBy");
                            int ContactIDCol = reader.GetOrdinal("ContactID");
                            int ContactCol = reader.GetOrdinal("Contact");
                            int CreatedByUserNameCol = reader.GetOrdinal("CreatedByUserName");
                            int ContactUserNameCol = reader.GetOrdinal("ContactUserName");

                            if (reader.Read())
                            {
                                t.Id = GetDataSafe(reader, IdCol, reader.GetInt32);
                                t.Title = GetDataSafe(reader, TitleCol, reader.GetString);
                                t.StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime); // Needed explicit cast because wat?
                                t.EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime);
                                t.Description = GetDataSafe(reader, DescCol, reader.GetString);
                                t.CreatedBy = new User { Id = GetDataSafe(reader, CreatedByNameCol, reader.GetString), UserName = GetDataSafe(reader, CreatedByUserNameCol, reader.GetString) };
                                t.Contact = new User { Id = GetDataSafe(reader, ContactCol, reader.GetString), UserName = GetDataSafe(reader, ContactUserNameCol, reader.GetString) };

                            }
                        }
                    }
                }
            }
            return t;
        }

        public List<Meeting> ReadAll()
        {
            List<Meeting> users = new List<Meeting>();

            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; BEGIN TRANSACTION SELECT User1.UserName CreatedByUserName, User2.UserName ContactUserName, Meetings.Id mId, Meetings.Deleted, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id; COMMIT";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int TitleCol = reader.GetOrdinal("Title");
                            int StartTimeCol = reader.GetOrdinal("StartTime");
                            int EndTimeCol = reader.GetOrdinal("EndTime");
                            int DescCol = reader.GetOrdinal("Description");
                            int CreatedByIDCol = reader.GetOrdinal("CreatedByID");
                            int CreatedByNameCol = reader.GetOrdinal("CreatedBy");
                            int ContactIDCol = reader.GetOrdinal("ContactID");
                            int ContactCol = reader.GetOrdinal("Contact");
                            int DeletedCol = reader.GetOrdinal("Deleted");
                            int IdCol = reader.GetOrdinal("mId");
                            int CreatedByUserNameCol = reader.GetOrdinal("CreatedByUserName");
                            int ContactUserNameCol = reader.GetOrdinal("ContactUserName");

                            while (reader.Read())
                            {
                                users.Add(new Meeting
                                {
                                    Id = GetDataSafe<int>(reader, IdCol, reader.GetInt32),
                                    Title = GetDataSafe(reader, TitleCol, reader.GetString),
                                    StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime),
                                    EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime),
                                    Description = GetDataSafe(reader, DescCol, reader.GetString),
                                    CreatedBy = new User { Id = GetDataSafe(reader, CreatedByNameCol, reader.GetString), UserName = GetDataSafe(reader, CreatedByUserNameCol, reader.GetString) },
                                    Deleted = GetDataSafe(reader, DeletedCol, reader.GetBoolean),
                                    Contact = new User { Id = GetDataSafe(reader, ContactCol, reader.GetString), UserName = GetDataSafe(reader, ContactUserNameCol, reader.GetString) }
                                });
                            }
                        }
                    }
                }
            }
            return users;
        }

        public Meeting AddUserToMeeting(Meeting m, User u)
        {
            string sql = "INSERT INTO Meetings_Users VALUES(@MeetingID, @UserID);";

            SqlParameter[] arrayofparams =
            {
            new SqlParameter { ParameterName = "@UserID", SqlValue = u.Id, SqlDbType = SqlDbType.NVarChar },
            new SqlParameter { ParameterName = "@MeetingID", SqlValue = m.Id, SqlDbType = SqlDbType.NVarChar },
            };
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(arrayofparams);
                    command.Connection.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    if (rowsaffected < 1)
                    {
                        throw new Exception();
                    }
                }
            }
            return m;
        }

        public Meeting Update(Meeting t)
        {
            string sql = "UPDATE Meetings SET Title = @title, Description = @description, StartTime = @starttime, EndTime = @endtime, CreatedByID = @createdbyid, ContactID = @contactid, Deleted = @deleted WHERE ID = @id";
            SqlParameter[] sqlparams =
            {
                new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@title", SqlValue = t.Title, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@description", SqlValue = t.Description, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@createdbyid", SqlValue = t.CreatedBy.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@contactid", SqlValue = t.Contact.Id, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@deleted", SqlValue = t.Deleted, SqlDbType = SqlDbType.Bit }
            };
            SqlConnection con = new SqlConnection(connectionstring);
            SqlCommand sqlcommand = new SqlCommand(sql, con);
            con.Open();
            SqlTransaction myTrans = con.BeginTransaction(IsolationLevel.RepeatableRead);
            sqlcommand.Parameters.AddRange(sqlparams);
            sqlcommand.Transaction = myTrans;
                int rowsaffected = sqlcommand.ExecuteNonQuery();
                if (rowsaffected < 1)
                {
                    throw new Exception();
                }
            myTrans.Commit();
            return t;
        }

        public Meeting ReadDetails(Meeting m) 
        {
            Meeting result = null;

            string sql = "SELECT Contact.UserName ContactUserName, CreatedBy.UserName CreatedByUserName, Meetings.id MeetingId, Contact.Id ContactId, CreatedBy.Id CreatedById, Meetings.starttime MeetingStartTime, Meetings.EndTime MeetingEndTime, Meetings.Title MeetingTitle, Meetings.Description, Meetings.Deleted, Contact.FirstName ContactFirstName, Contact.LastName ContactLastName, CreatedBy.FirstName CreatedByFirstName, CreatedBy.LastName CreatedByLastName FROM Meetings JOIN AspNetUsers Contact ON Meetings.ContactID = Contact.Id JOIN AspNetUsers CreatedBy ON Meetings.CreatedByID = CreatedBy.Id WHERE Meetings.id = @id";

            try
            {
                SqlParameter midparam = new SqlParameter() {ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = m.Id};
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(midparam);
                        command.Connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = (new Meeting
                                {
                                    Id = (int) reader["MeetingId"],
                                    Deleted = (bool) reader["Deleted"],
                                    Title = reader["MeetingTitle"].ToString(),
                                    StartTime = (DateTime) reader["MeetingStartTime"],
                                    EndTime = (DateTime) reader["MeetingEndTime"],
                                    Description = reader["Description"].ToString(),
                                    CreatedBy = new User
                                    {
                                        Id = reader["CreatedById"].ToString(),
                                        FirstName = reader["CreatedByFirstName"].ToString(),
                                        LastName = reader["CreatedByLastName"].ToString(),
                                        UserName = reader["CreatedByUserName"].ToString()
                                    },
                                    Contact = new User
                                    {
                                        FirstName = reader["ContactFirstName"].ToString(),
                                        LastName = reader["ContactLastName"].ToString(),
                                        Id = reader["ContactId"].ToString(),
                                        UserName = reader["ContactUserName"].ToString()
                                    }
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new FaultException<SqlException>(e, new FaultReason(e.Message), new FaultCode("Sender"));
                //log exception   
            }
            catch(DbException e)
            {
                throw new FaultException<DbException>(e, new FaultReason(e.Message), new FaultCode("Sender"));
                //log exception!
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, new FaultReason(e.Message), new FaultCode("Sender"));
                //log Exception!
            }
            return result;
        }

        public T GetDataSafe<T>(SqlDataReader reader, int columnIndex, Func<int, T> getData)
        {
            if (!reader.IsDBNull(columnIndex))
            {
                return getData(columnIndex);
            }
            return default(T);
        }
    }
}
