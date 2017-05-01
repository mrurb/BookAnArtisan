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
            string query = "INSERT INTO Meeting(StartTime, EndTime, Title, Description, ContactID, CreatedByID) VALUES(@StartTime, @EndTime, @Title, @Description, @ContactID, @CreatedByID);";
            SqlParameter[] ArrayOfParams =
            {
                new SqlParameter { ParameterName = "@StartTime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@EndTime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@Title", SqlValue = t.Title, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@Description", SqlValue = t.Description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@ContactID", SqlValue = t.ContactId, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@CreatedByID", SqlValue = t.CreatedById, SqlDbType = SqlDbType.NVarChar },
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

            string sql = "SELECT * FROM Meeting_Users JOIN Meeting ON Meeting_Users.MeetingID = Meeting.ID WHERE Meeting_Users.UserID = @Id";

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
                            int CreatedByIDCol = reader.GetOrdinal("CreatedByID");
                            int ContactIDCol = reader.GetOrdinal("ContactID");
                            int IdCol = reader.GetOrdinal("Id");

                            while (reader.Read())
                            {
                                userMeetings.Add(new Meeting
                                {
                                    Title = GetDataSafe(reader, TitleCol, reader.GetString),
                                    StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime),
                                    EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime),
                                    Description = GetDataSafe(reader, DescCol, reader.GetString),
                                    CreatedById = GetDataSafe(reader, CreatedByIDCol, reader.GetString),
                                    ContactId = GetDataSafe(reader, ContactIDCol, reader.GetString),
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
            string sql = "UPDATE Meeting SET Deleted = 1 WHERE ID = @Id";

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
            string sql = "SELECT * FROM Meeting WHERE ID = @Id"; // search by ID, see below.

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
                            int IdCol = reader.GetOrdinal("ID"); // probably don't want ID at all... but how to search then boss?
                            int TitleCol = reader.GetOrdinal("Title");
                            int StartTimeCol = reader.GetOrdinal("StartTime");
                            int EndTimeCol = reader.GetOrdinal("EndTime");
                            int DescCol = reader.GetOrdinal("Description");
                            int CreatedByIDCol = reader.GetOrdinal("CreatedByID");
                            int ContactIDCol = reader.GetOrdinal("ContactID");

                            if (reader.Read())
                            {
                                t.Id = GetDataSafe(reader, IdCol, reader.GetInt32);
                                t.Title = GetDataSafe(reader, TitleCol, reader.GetString);
                                t.StartTime = (DateTime)GetDataSafe(reader, StartTimeCol, reader.GetSqlDateTime); // Needed explicit cast because wat?
                                t.EndTime = (DateTime)GetDataSafe(reader, EndTimeCol, reader.GetSqlDateTime);
                                t.Description = GetDataSafe(reader, DescCol, reader.GetString);
                                t.CreatedById = GetDataSafe(reader, CreatedByIDCol, reader.GetString);
                                t.ContactId = GetDataSafe(reader, ContactIDCol, reader.GetString);

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

            string sql = "SELECT Meeting.Id mId, StartTime, EndTime, Title, Description, User1.UserName CreatedBy, User1.Id CreatedByID, User2.UserName Contact, User2.Id ContactID FROM Meeting JOIN AspNetUsers User1 ON Meeting.CreatedByID = User1.Id JOIN AspNetUsers User2 ON Meeting.ContactID = User2.Id;";

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
                                    CreatedById = GetDataSafe(reader, CreatedByIDCol, reader.GetString),
                                    CreatedBy = GetDataSafe(reader, CreatedByNameCol, reader.GetString),
                                    ContactId = GetDataSafe(reader, ContactIDCol, reader.GetString),
                                    Deleted = GetDataSafe(reader, DeletedCol, reader.GetBoolean),
                                    Contact = GetDataSafe(reader, ContactCol, reader.GetString)
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
            string sql = "INSERT INTO Meeting_Users VALUES(@MeetingID, @UserID);";

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
            string sql = "UPDATE Meeting SET Title = @title, Description = @description, StartTime = @starttime, EndTime = @endtime, CreatedByID = @createdbyid, ContactID = @contactid, Deleted = @deleted WHERE ID = @id";
            SqlParameter[] sqlparams =
            {
                new SqlParameter { ParameterName = "@id", SqlValue = t.Id, SqlDbType = SqlDbType.Int },
                new SqlParameter { ParameterName = "@title", SqlValue = t.Title, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@description", SqlValue = t.Description, SqlDbType = SqlDbType.Text },
                new SqlParameter { ParameterName = "@starttime", SqlValue = t.StartTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@endtime", SqlValue = t.EndTime, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@createdbyid", SqlValue = t.CreatedById, SqlDbType = SqlDbType.NVarChar },
                new SqlParameter { ParameterName = "@contactid", SqlValue = t.ContactId, SqlDbType = SqlDbType.NVarChar },
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

            string sql = "SELECT meeting.id mid, Contact.Id cid, Created_By.Id cbid, meeting.starttime mst, meeting.EndTime met, meeting.Title mt, meeting.Description, Meeting.Deleted, Contact.FirstName cf, Contact.LastName cl, Created_By.FirstName cbf, Created_By.LastName cbl FROM Meeting JOIN AspNetUsers Contact ON Meeting.ContactID = Contact.Id JOIN AspNetUsers Created_By ON Meeting.CreatedByID = Created_By.Id WHERE meeting.id = @id";

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
                                CreatedById = reader["cbid"].ToString(),
                                CreatedBy = reader["cbf"].ToString() + " " + reader["cbl"].ToString(),
                                ContactId = reader["cid"].ToString(),
                                Contact = reader["cf"].ToString() + " " + reader["cl"].ToString()
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
