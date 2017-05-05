using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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

        public List<Meeting> ReadAllForUser(User user)
        {
            List<Meeting> userMeetings = new List<Meeting>();

            string sql = "SELECT DISTINCT Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID  FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Meetings.CreatedByID = @Id OR Meetings.ContactID = @Id";

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
                            int contactCol = reader.GetOrdinal("Contact");
                            int createdbyCol = reader.GetOrdinal("CreatedBy");

                            while (reader.Read())
                            {
                                userMeetings.Add(new Meeting
                                {
                                    Title = GetDataSafe(reader, TitleCol, reader.GetString),
                                    StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime),
                                    EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime),
                                    Description = GetDataSafe(reader, DescCol, reader.GetString),
                                    CreatedBy = new User { Id = GetDataSafe(reader, CreatedByCol, reader.GetString) },
                                    Contact = new User { Id = GetDataSafe(reader, ContactCol, reader.GetString) },
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
            string sql = "SELECT Meetings.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id WHERE Meetings.ID = @Id"; // search by ID, see below.

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

                            if (reader.Read())
                            {
                                t.Id = GetDataSafe(reader, IdCol, reader.GetInt32);
                                t.Title = GetDataSafe(reader, TitleCol, reader.GetString);
                                t.StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime); // Needed explicit cast because wat?
                                t.EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime);
                                t.Description = GetDataSafe(reader, DescCol, reader.GetString);
                                t.CreatedBy = new User {Id =  GetDataSafe(reader, CreatedByNameCol, reader.GetString) };
                                t.Contact = new User {Id =  GetDataSafe(reader, ContactCol, reader.GetString) };

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

            string sql = "SELECT Meetings.Id mId, Meetings.Deleted, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meetings JOIN AspNetUsers User1 ON Meetings.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meetings.ContactID = User2.Id;";

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

                            while (reader.Read())
                            {
                                users.Add(new Meeting
                                {
                                    Id = GetDataSafe<int>(reader, IdCol, reader.GetInt32),
                                    Title = GetDataSafe(reader, TitleCol, reader.GetString),
                                    StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime),
                                    EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime),
                                    Description = GetDataSafe(reader, DescCol, reader.GetString),
                                    CreatedBy = new User { Id = GetDataSafe(reader, CreatedByNameCol, reader.GetString) },
                                    Deleted = GetDataSafe(reader, DeletedCol, reader.GetBoolean),
                                    Contact = new User { Id = GetDataSafe(reader, ContactCol, reader.GetString) }
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

        public Meeting ReadDetails(Meeting m)
        {
            Meeting result = null;

            string sql = "SELECT Meetings.id mid, Contact.Id cid, Created_By.Id cbid, Meetings.starttime mst, Meetings.EndTime met, Meetings.Title mt, Meetings.Description, Meeting.Deleted, Contact.FirstName cf, Contact.LastName cl, Created_By.FirstName cbf, Created_By.LastName cbl FROM Meetings JOIN AspNetUsers Contact ON Meetings.ContactID = Contact.Id JOIN AspNetUsers Created_By ON Meetings.CreatedByID = Created_By.Id WHERE Meetings.id = @id";

            SqlParameter midparam = new SqlParameter() { ParameterName = "@id", SqlDbType= SqlDbType.Int, Value= m.Id };
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
                                Id = (int)reader["mid"],
                                Deleted = (bool)reader["Deleted"],
                                Title = reader["mt"].ToString(),
                                StartTime = (DateTime)reader["mst"],
                                EndTime = (DateTime)reader["met"],
                                Description = reader["Description"].ToString(),
                                CreatedBy = new User { Id =  reader["cbid"].ToString(), FirstName = reader["cbf"].ToString(), LastName = reader["cbl"].ToString() },
                                Contact = new User { FirstName = reader["cf"].ToString(), LastName =  reader["cl"].ToString(), Id = reader["cid"].ToString() }
                            });
                        }
                    }
                }
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
